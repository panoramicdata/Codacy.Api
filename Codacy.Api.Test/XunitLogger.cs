using System.Globalization;

namespace Codacy.Api.Test;

/// <summary>
/// Logger that writes to xUnit test output
/// </summary>
public class XunitLogger(ITestOutputHelper output, string categoryName, LogLevel minLevel = LogLevel.Debug) : ILogger
{
	private readonly ITestOutputHelper _output = output;
	private readonly string _categoryName = categoryName;
	private readonly LogLevel _minLevel = minLevel;

	public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;

	public bool IsEnabled(LogLevel logLevel) => logLevel >= _minLevel;

	public void Log<TState>(
		LogLevel logLevel,
		EventId eventId,
		TState state,
		Exception? exception,
		Func<TState, Exception?, string> formatter)
	{
		if (!IsEnabled(logLevel))
		{
			return;
		}

		var message = formatter(state, exception);
		var timestamp = DateTimeOffset.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture);
		var logLevelString = logLevel switch
		{
			LogLevel.Trace => "TRACE",
			LogLevel.Debug => "DEBUG",
			LogLevel.Information => "INFO ",
			LogLevel.Warning => "WARN ",
			LogLevel.Error => "ERROR",
			LogLevel.Critical => "CRIT ",
			_ => "NONE "
		};

		var logOutput = $"[{timestamp}] [{logLevelString}] [{_categoryName}] {message}";

		if (eventId.Id != 0)
		{
			logOutput = $"[{timestamp}] [{logLevelString}] [{_categoryName}] [EventId:{eventId.Id}] {message}";
		}

		try
		{
			_output.WriteLine(logOutput);

			if (exception != null)
			{
				_output.WriteLine($"Exception: {exception}");
			}
		}
		catch
		{
			// If test output is not available, ignore
		}
	}
}

/// <summary>
/// Logger provider for xUnit test output
/// </summary>
public class XunitLoggerProvider(ITestOutputHelper output, LogLevel minLevel = LogLevel.Debug) : ILoggerProvider
{
	private readonly ITestOutputHelper _output = output;
	private readonly LogLevel _minLevel = minLevel;

	public ILogger CreateLogger(string categoryName)
	{
		return new XunitLogger(_output, categoryName, _minLevel);
	}

	public void Dispose()
	{
		GC.SuppressFinalize(this);
	}
}
