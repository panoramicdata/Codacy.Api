using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Codacy.Api;

/// <summary>
/// HTTP client handler that logs requests and responses for debugging purposes
/// </summary>
public partial class LoggingHttpClientHandler : DelegatingHandler
{
	private readonly CodacyClientOptions _options;
	private readonly ILogger _logger;

	/// <summary>
	/// Initializes a new instance of the <see cref="LoggingHttpClientHandler"/> class
	/// </summary>
	/// <param name="options">The client options containing logging configuration</param>
	public LoggingHttpClientHandler(CodacyClientOptions options)
		: base(new HttpClientHandler())
	{
		ArgumentNullException.ThrowIfNull(options);
		_options = options;
		_logger = options.Logger ?? NullLogger.Instance;
	}

	/// <summary>
	/// Sends an HTTP request with optional logging
	/// </summary>
	/// <param name="request">The HTTP request message</param>
	/// <param name="cancellationToken">A cancellation token</param>
	/// <returns>The HTTP response message</returns>
	protected override async Task<HttpResponseMessage> SendAsync(
		HttpRequestMessage request,
		CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(request);

		// Add api-token authentication header
		request.Headers.Add("api-token", _options.ApiToken);

		// Add any custom default headers
		foreach (var header in _options.DefaultHeaders)
		{
			request.Headers.TryAddWithoutValidation(header.Key, header.Value);
		}

		if (_options.EnableRequestLogging)
		{
			await LogRequestAsync(request, cancellationToken).ConfigureAwait(false);
		}

		var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

		if (_options.EnableResponseLogging)
		{
			await LogResponseAsync(response, cancellationToken).ConfigureAwait(false);
		}

		return response;
	}

	/// <summary>
	/// Logs HTTP request details
	/// </summary>
	private async Task LogRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
	{
		LogRequestStart();
		LogRequestMethod(request.Method.ToString());
		LogRequestUri(request.RequestUri?.ToString() ?? "(null)");
		LogRequestHeadersStart();

		foreach (var header in request.Headers)
		{
			var headerValue = string.Join(", ", header.Value);

			// Redact sensitive headers
			if (header.Key.Equals("api-token", StringComparison.OrdinalIgnoreCase) ||
				header.Key.Equals("Authorization", StringComparison.OrdinalIgnoreCase))
			{
				LogRequestHeaderRedacted(header.Key);
			}
			else
			{
				LogRequestHeader(header.Key, headerValue);
			}
		}

		if (request.Content != null)
		{
			var requestBody = await request.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
			if (!string.IsNullOrEmpty(requestBody))
			{
				LogRequestBody(requestBody);
			}
		}

		LogRequestEnd();
	}

	/// <summary>
	/// Logs HTTP response details
	/// </summary>
	private async Task LogResponseAsync(HttpResponseMessage response, CancellationToken cancellationToken)
	{
		LogResponseStart();
		LogResponseStatus((int)response.StatusCode, response.ReasonPhrase ?? string.Empty);
		LogResponseHeadersStart();

		foreach (var header in response.Headers)
		{
			var headerValue = string.Join(", ", header.Value);
			LogResponseHeader(header.Key, headerValue);
		}

		foreach (var header in response.Content.Headers)
		{
			var headerValue = string.Join(", ", header.Value);
			LogResponseHeader(header.Key, headerValue);
		}

		var responseBody = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
		if (!string.IsNullOrEmpty(responseBody))
		{
			LogResponseBody(responseBody);
		}

		LogResponseEnd();
	}

	// LoggerMessage delegates for high-performance logging
	[LoggerMessage(Level = LogLevel.Debug, Message = "=== HTTP Request ===")]
	private partial void LogRequestStart();

	[LoggerMessage(Level = LogLevel.Debug, Message = "  Method: {method}")]
	private partial void LogRequestMethod(string method);

	[LoggerMessage(Level = LogLevel.Debug, Message = "  URI: {uri}")]
	private partial void LogRequestUri(string uri);

	[LoggerMessage(Level = LogLevel.Debug, Message = "  Headers:")]
	private partial void LogRequestHeadersStart();

	[LoggerMessage(Level = LogLevel.Debug, Message = "    {headerName}: ***REDACTED***")]
	private partial void LogRequestHeaderRedacted(string headerName);

	[LoggerMessage(Level = LogLevel.Debug, Message = "    {headerName}: {headerValue}")]
	private partial void LogRequestHeader(string headerName, string headerValue);

	[LoggerMessage(Level = LogLevel.Debug, Message = "  Body: {requestBody}")]
	private partial void LogRequestBody(string requestBody);

	[LoggerMessage(Level = LogLevel.Debug, Message = "=== End HTTP Request ===")]
	private partial void LogRequestEnd();

	[LoggerMessage(Level = LogLevel.Debug, Message = "=== HTTP Response ===")]
	private partial void LogResponseStart();

	[LoggerMessage(Level = LogLevel.Debug, Message = "  Status: {statusCode} {reasonPhrase}")]
	private partial void LogResponseStatus(int statusCode, string reasonPhrase);

	[LoggerMessage(Level = LogLevel.Debug, Message = "  Headers:")]
	private partial void LogResponseHeadersStart();

	[LoggerMessage(Level = LogLevel.Debug, Message = "    {headerName}: {headerValue}")]
	private partial void LogResponseHeader(string headerName, string headerValue);

	[LoggerMessage(Level = LogLevel.Debug, Message = "  Body: {responseBody}")]
	private partial void LogResponseBody(string responseBody);

	[LoggerMessage(Level = LogLevel.Debug, Message = "=== End HTTP Response ===")]
	private partial void LogResponseEnd();
}
