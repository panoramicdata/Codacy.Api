using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using System.Text.Json;

namespace Codacy.Api.Test;

/// <summary>
/// Delegating handler that logs all HTTP requests and responses with full details
/// </summary>
public class LoggingHttpMessageHandler : DelegatingHandler
{
	private readonly ILogger _logger;
	private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };

	public LoggingHttpMessageHandler(ILogger logger)
		: base(new HttpClientHandler())
	{
		_logger = logger;
	}

	public LoggingHttpMessageHandler(ILogger logger, HttpMessageHandler innerHandler)
		: base(innerHandler)
	{
		_logger = logger;
	}

	protected override async Task<HttpResponseMessage> SendAsync(
		HttpRequestMessage request,
		CancellationToken cancellationToken)
	{
		var requestId = Guid.NewGuid();

		// Log Request
		await LogRequestAsync(requestId, request);

		// Send request
		var startTime = DateTimeOffset.UtcNow;
		HttpResponseMessage? response = null;
		Exception? exception = null;

		try
		{
			response = await base.SendAsync(request, cancellationToken);
			return response;
		}
		catch (Exception ex)
		{
			exception = ex;
			throw;
		}
		finally
		{
			var duration = DateTimeOffset.UtcNow - startTime;

			// Log Response or Exception
			if (response != null)
			{
				await LogResponseAsync(requestId, response, duration);
			}
			else if (exception != null)
			{
				LogException(requestId, exception, duration);
			}
		}
	}

	[SuppressMessage("Globalization", "CA1305:Specify IFormatProvider", Justification = "Logging output doesn't require culture-specific formatting")]
	private async Task LogRequestAsync(Guid requestId, HttpRequestMessage request)
	{
		var sb = new StringBuilder();
		sb.AppendLine($"");
		sb.AppendLine($"HTTP REQUEST [{requestId}]");
		sb.AppendLine($"{request.Method} {request.RequestUri}");
		sb.AppendLine($"HTTP/{request.Version}");
		sb.AppendLine($"");
		sb.AppendLine($"HEADERS:");

		foreach (var header in request.Headers)
		{
			foreach (var value in header.Value)
			{
				// Mask sensitive headers
				var displayValue = header.Key.Equals("api-token", StringComparison.OrdinalIgnoreCase) ||
								   header.Key.Equals("Authorization", StringComparison.OrdinalIgnoreCase)
					? MaskToken(value)
					: value;
				sb.AppendLine($"  {header.Key}: {displayValue}");
			}
		}

		// Log request content if present
		if (request.Content != null)
		{
			sb.AppendLine($"");
			sb.AppendLine($"CONTENT HEADERS:");
			foreach (var header in request.Content.Headers)
			{
				foreach (var value in header.Value)
				{
					sb.AppendLine($"  {header.Key}: {value}");
				}
			}

			sb.AppendLine($"");
			sb.AppendLine($"BODY:");
			try
			{
				var content = await request.Content.ReadAsStringAsync();
				if (!string.IsNullOrEmpty(content))
				{
					// Format JSON for readability
					var formattedContent = FormatJson(content);
					sb.AppendLine(formattedContent);
				}
				else
				{
					sb.AppendLine($"  (empty)");
				}
			}
			catch (Exception ex)
			{
				sb.AppendLine($"  (unable to read content: {ex.Message})");
			}
		}

		sb.AppendLine($"");

		LogMessage(sb.ToString());
	}

	[SuppressMessage("Globalization", "CA1305:Specify IFormatProvider", Justification = "Logging output doesn't require culture-specific formatting")]
	private async Task LogResponseAsync(Guid requestId, HttpResponseMessage response, TimeSpan duration)
	{
		var sb = new StringBuilder();
		sb.AppendLine($"");
		sb.AppendLine($"HTTP RESPONSE [{requestId}]");
		sb.AppendLine($"HTTP/{response.Version} {(int)response.StatusCode} {response.ReasonPhrase}");
		sb.AppendLine($"Duration: {duration.TotalMilliseconds.ToString("F2", CultureInfo.InvariantCulture)}ms");
		sb.AppendLine($"");
		sb.AppendLine($"HEADERS:");

		foreach (var header in response.Headers)
		{
			foreach (var value in header.Value)
			{
				sb.AppendLine($"  {header.Key}: {value}");
			}
		}

		// Log response content if present
		if (response.Content != null)
		{
			sb.AppendLine($"");
			sb.AppendLine($"CONTENT HEADERS:");
			foreach (var header in response.Content.Headers)
			{
				foreach (var value in header.Value)
				{
					sb.AppendLine($"  {header.Key}: {value}");
				}
			}

			sb.AppendLine($"");
			sb.AppendLine($"BODY:");
			try
			{
				var content = await response.Content.ReadAsStringAsync();
				if (!string.IsNullOrEmpty(content))
				{
					// Format JSON for readability
					var formattedContent = FormatJson(content);
					var lines = formattedContent.Split('\n');

					// Limit output for very long responses
					const int maxLines = 100;
					var lineCount = 0;
					foreach (var line in lines)
					{
						if (lineCount >= maxLines)
						{
							sb.AppendLine($"... ({lines.Length - maxLines} more lines)");
							break;
						}
						sb.AppendLine(line);
						lineCount++;
					}
				}
				else
				{
					sb.AppendLine($"  (empty)");
				}
			}
			catch (Exception ex)
			{
				sb.AppendLine($"  (unable to read content: {ex.Message})");
			}
		}

		sb.AppendLine($"");

		var logLevel = response.IsSuccessStatusCode ? LogLevel.Debug : LogLevel.Warning;
		LogMessage(sb.ToString(), logLevel);
	}

	[SuppressMessage("Globalization", "CA1305:Specify IFormatProvider", Justification = "Logging output doesn't require culture-specific formatting")]
	private void LogException(Guid requestId, Exception exception, TimeSpan duration)
	{
		var sb = new StringBuilder();
		sb.AppendLine($"");
		sb.AppendLine($"HTTP EXCEPTION [{requestId}]");
		sb.AppendLine($"Duration: {duration.TotalMilliseconds.ToString("F2", CultureInfo.InvariantCulture)}ms");
		sb.AppendLine($"Exception: {exception.GetType().Name}");
		sb.AppendLine($"Message: {exception.Message}");
		sb.AppendLine($"");

		LogMessage(sb.ToString(), LogLevel.Error);
	}

	[SuppressMessage("Usage", "CA2254:Template should be a static expression", Justification = "Dynamic log messages for HTTP request/response logging")]
	[SuppressMessage("Performance", "CA1848:Use LoggerMessage delegates", Justification = "Simple logging implementation for test infrastructure")]
	private void LogMessage(string message, LogLevel logLevel = LogLevel.Debug)
	{
		_logger.Log(logLevel, message);
	}

	private static string MaskToken(string token)
	{
		if (string.IsNullOrEmpty(token) || token.Length <= 8)
		{
			return "***";
		}

		return $"{token[..4]}...{token[^4..]}";
	}

	private static string FormatJson(string json)
	{
		try
		{
			// Simple JSON formatting - add proper indentation
			var formatted = JsonSerializer.Serialize(
				JsonSerializer.Deserialize<object>(json),
				JsonOptions);
			return formatted;
		}
		catch
		{
			// If not valid JSON or formatting fails, return as-is
			return json;
		}
	}
}
