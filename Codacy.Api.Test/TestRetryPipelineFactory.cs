using Polly;
using Polly.Retry;
using Refit;
using System.Net;

#pragma warning disable CA1848 // Test diagnostics do not require source-generated logging

namespace Codacy.Api.Test;

internal static class TestRetryPipelineFactory
{
	public static ResiliencePipeline Create(ILogger? logger, int maxRetries)
	{
		var options = new RetryStrategyOptions
		{
			MaxRetryAttempts = maxRetries,
			Delay = TimeSpan.FromMilliseconds(TestDataManager.DefaultRetryDelayMs),
			BackoffType = DelayBackoffType.Exponential,
			UseJitter = true,
			ShouldHandle = new PredicateBuilder()
				.Handle<ApiException>(IsTransientError)
				.Handle<HttpRequestException>()
				.Handle<TaskCanceledException>(),
			OnRetry = args => LogRetryAttempt(logger, args, maxRetries)
		};

		return new ResiliencePipelineBuilder().AddRetry(options).Build();
	}

	private static bool IsTransientError(ApiException exception) =>
		exception.StatusCode is HttpStatusCode.TooManyRequests or
			HttpStatusCode.ServiceUnavailable or
			HttpStatusCode.GatewayTimeout or
			HttpStatusCode.RequestTimeout ||
		(int)exception.StatusCode >= 500;

	private static ValueTask LogRetryAttempt(
		ILogger? logger,
		OnRetryArguments<object> args,
		int maxRetries)
	{
		logger?.LogWarning(
			args.Outcome.Exception,
			"Request failed. Retry {RetryCount} of {MaxRetries} after {Delay}ms. Error: {Message}",
			args.AttemptNumber,
			maxRetries,
			args.RetryDelay.TotalMilliseconds,
			args.Outcome.Exception?.Message ?? "Unknown error");
		return ValueTask.CompletedTask;
	}
}