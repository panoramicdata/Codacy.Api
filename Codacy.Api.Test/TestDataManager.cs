#pragma warning disable CA1848 // Use LoggerMessage delegates for improved performance
#pragma warning disable CA1510 // Use ArgumentNullException.ThrowIfNull
#pragma warning disable S2360 // Optional parameters should not be used - This is a test helper class where optional parameters improve usability

using Polly;
using Polly.Retry;
using Refit;
using System.Net;

namespace Codacy.Api.Test;

/// <summary>
/// Manages test data lifecycle for integration tests, including seeding, cleanup, and retry logic
/// </summary>
public class TestDataManager : IDisposable
{
	private readonly CodacyClient _client;
	private readonly ILogger? _logger;
	private readonly ResiliencePipeline _retryPipeline;
	private readonly List<Action> _cleanupActions = [];
	private bool _disposed;

	// Test data configuration
	private readonly string _testOrganization;
	private readonly string _testRepository;
	private readonly Provider _testProvider;

	/// <summary>
	/// Default retry policy configuration
	/// </summary>
	internal const int DefaultMaxRetries = 3;
	internal const int DefaultRetryDelayMs = 1000;
	internal const int DefaultRetryBackoffMultiplier = 2;

	/// <summary>
	/// Initializes a new instance of the TestDataManager
	/// </summary>
	/// <param name="client">Codacy API client</param>
	/// <param name="testOrganization">Test organization name</param>
	/// <param name="testRepository">Test repository name</param>
	/// <param name="testProvider">Test provider (e.g., GitHub)</param>
	/// <param name="logger">Optional logger for diagnostics</param>
	/// <param name="maxRetries">Maximum number of retry attempts</param>
	public TestDataManager(
		CodacyClient client,
		string testOrganization,
		string testRepository,
		Provider testProvider,
		ILogger? logger = null,
		int maxRetries = DefaultMaxRetries)
	{
		_client = client ?? throw new ArgumentNullException(nameof(client));
		_testOrganization = testOrganization ?? throw new ArgumentNullException(nameof(testOrganization));
		_testRepository = testRepository ?? throw new ArgumentNullException(nameof(testRepository));
		_testProvider = testProvider;
		_logger = logger;
		_retryPipeline = BuildRetryPipeline(maxRetries);
	}

	private ResiliencePipeline BuildRetryPipeline(int maxRetries)
	{
		return new ResiliencePipelineBuilder()
			.AddRetry(CreateRetryOptions(maxRetries))
			.Build();
	}

	private RetryStrategyOptions CreateRetryOptions(int maxRetries)
	{
		return new RetryStrategyOptions
		{
			MaxRetryAttempts = maxRetries,
			Delay = TimeSpan.FromMilliseconds(DefaultRetryDelayMs),
			BackoffType = DelayBackoffType.Exponential,
			UseJitter = true,
			ShouldHandle = new PredicateBuilder()
				.Handle<ApiException>(IsTransientError)
				.Handle<HttpRequestException>()
				.Handle<TaskCanceledException>(),
			OnRetry = args => LogRetryAttempt(args, maxRetries)
		};
	}

	private static bool IsTransientError(ApiException ex)
	{
		return ex.StatusCode == HttpStatusCode.TooManyRequests ||
			ex.StatusCode == HttpStatusCode.ServiceUnavailable ||
			ex.StatusCode == HttpStatusCode.GatewayTimeout ||
			ex.StatusCode == HttpStatusCode.RequestTimeout ||
			(int)ex.StatusCode >= 500;
	}

	private ValueTask LogRetryAttempt(OnRetryArguments<object> args, int maxRetries)
	{
		_logger?.LogWarning(
			args.Outcome.Exception,
			"Request failed. Retry {RetryCount} of {MaxRetries} after {Delay}ms. Error: {Message}",
			args.AttemptNumber, maxRetries, args.RetryDelay.TotalMilliseconds,
			args.Outcome.Exception?.Message ?? "Unknown error");
		return ValueTask.CompletedTask;
	}

	#region Test Data Verification

	/// <summary>
	/// Verifies that the test repository exists in Codacy
	/// </summary>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>True if repository exists, false otherwise</returns>
	public async Task<bool> VerifyRepositoryExistsAsync(CancellationToken cancellationToken = default)
	{
		try
		{
			await ExecuteWithRetryAsync(async () =>
			{
				var response = await _client.Repositories.GetRepositoryAsync(
					_testProvider,
					_testOrganization,
					_testRepository,
					cancellationToken);
				return response.Data != null;
			}, cancellationToken);

			_logger?.LogInformation(
				"Repository {Organization}/{Repository} verified in Codacy",
				_testOrganization, _testRepository);
			return true;
		}
		catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
		{
			_logger?.LogWarning(
				"Repository {Organization}/{Repository} not found in Codacy",
				_testOrganization, _testRepository);
			return false;
		}
	}

	/// <summary>
	/// Verifies that the test repository has been analyzed
	/// </summary>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>True if repository has analysis data, false otherwise</returns>
	public async Task<bool> VerifyRepositoryAnalyzedAsync(CancellationToken cancellationToken = default)
	{
		try
		{
			var hasFiles = await ExecuteWithRetryAsync(async () =>
			{
				var response = await _client.Repositories.ListFilesAsync(
					_testProvider,
					_testOrganization,
					_testRepository,
					null,
					null,
					null,
					null,
					null,
					null,
					cancellationToken);
				return response.Data?.Count > 0;
			}, cancellationToken);

			if (hasFiles)
			{
				_logger?.LogInformation(
					"Repository {Organization}/{Repository} has analysis data",
					_testOrganization, _testRepository);
			}
			else
			{
				_logger?.LogWarning(
					"Repository {Organization}/{Repository} has no analysis data",
					_testOrganization, _testRepository);
			}

			return hasFiles;
		}
		catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
		{
			_logger?.LogWarning(
				"Repository {Organization}/{Repository} not found or not analyzed",
				_testOrganization, _testRepository);
			return false;
		}
	}

	/// <summary>
	/// Verifies that the test repository has branches
	/// </summary>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>True if repository has branches, false otherwise</returns>
	public async Task<bool> VerifyRepositoryHasBranchesAsync(CancellationToken cancellationToken = default)
	{
		try
		{
			var hasBranches = await ExecuteWithRetryAsync(async () =>
			{
				var response = await _client.Repositories.ListRepositoryBranchesAsync(
					_testProvider,
					_testOrganization,
					_testRepository,
					null,
					null,
					null,
					null,
					null,
					null,
					cancellationToken);
				return response.Data?.Count > 0;
			}, cancellationToken);

			if (hasBranches)
			{
				_logger?.LogInformation(
					"Repository {Organization}/{Repository} has branches",
					_testOrganization, _testRepository);
			}
			else
			{
				_logger?.LogWarning(
					"Repository {Organization}/{Repository} has no branches",
					_testOrganization, _testRepository);
			}

			return hasBranches;
		}
		catch (ApiException ex) when (
			ex.StatusCode == HttpStatusCode.NotFound ||
			ex.StatusCode == HttpStatusCode.BadRequest)
		{
			_logger?.LogWarning(
				"Repository {Organization}/{Repository} branches not available: {Message}",
				_testOrganization, _testRepository, ex.Message);
			return false;
		}
	}

	#endregion

	#region Test Data Retrieval

	/// <summary>
	/// Gets the test repository details
	/// </summary>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Repository details</returns>
	public Task<Repository?> GetTestRepositoryAsync(CancellationToken cancellationToken = default)
	{
		return ExecuteWithRetryAsync(async () =>
		{
			try
			{
				var response = await _client.Repositories.GetRepositoryAsync(
					_testProvider,
					_testOrganization,
					_testRepository,
					cancellationToken);
				return response.Data;
			}
			catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
			{
				_logger?.LogWarning("Test repository not found in Codacy");
				return null;
			}
		}, cancellationToken);
	}

	/// <summary>
	/// Gets a list of branches for the test repository
	/// </summary>
	/// <param name="limit">Maximum number of branches to retrieve</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>List of branches</returns>
	public Task<List<Branch>> GetTestRepositoryBranchesAsync(
		int? limit = null,
		CancellationToken cancellationToken = default)
	{
		return ExecuteWithRetryAsync(async () =>
		{
			try
			{
				var response = await _client.Repositories.ListRepositoryBranchesAsync(
					_testProvider,
					_testOrganization,
					_testRepository,
					null,
					null,
					limit,
					null,
					null,
					null,
					cancellationToken);
				return response.Data ?? [];
			}
			catch (ApiException ex) when (
				ex.StatusCode == HttpStatusCode.NotFound ||
				ex.StatusCode == HttpStatusCode.BadRequest)
			{
				_logger?.LogWarning("Test repository branches not available: {Message}", ex.Message);
				return [];
			}
		}, cancellationToken);
	}

	/// <summary>
	/// Gets the default branch for the test repository
	/// </summary>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Default branch or null if not found</returns>
	public async Task<Branch?> GetDefaultBranchAsync(CancellationToken cancellationToken = default)
	{
		var branches = await GetTestRepositoryBranchesAsync(cancellationToken: cancellationToken);
		return branches.FirstOrDefault(b => b.IsDefault);
	}

	/// <summary>
	/// Gets a list of files for the test repository
	/// </summary>
	/// <param name="branch">Optional branch name</param>
	/// <param name="limit">Maximum number of files to retrieve</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>List of files</returns>
	public Task<List<FileWithAnalysisInfo>> GetTestRepositoryFilesAsync(
		string? branch = null,
		int? limit = null,
		CancellationToken cancellationToken = default)
	{
		return ExecuteWithRetryAsync(async () =>
		{
			try
			{
				var response = await _client.Repositories.ListFilesAsync(
					_testProvider,
					_testOrganization,
					_testRepository,
					branch,
					null,
					null,
					null,
					null,
					limit,
					cancellationToken);
				return response.Data ?? [];
			}
			catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
			{
				_logger?.LogWarning("Test repository files not available: {Message}", ex.Message);
				return [];
			}
		}, cancellationToken);
	}

	#endregion

	#region Retry Logic

	/// <summary>
	/// Executes an async function with retry logic for transient failures
	/// </summary>
	/// <typeparam name="T">Return type</typeparam>
	/// <param name="operation">Operation to execute</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Operation result</returns>
	public async Task<T> ExecuteWithRetryAsync<T>(
		Func<Task<T>> operation,
		CancellationToken cancellationToken = default)
	{
		return await _retryPipeline.ExecuteAsync(async (ct) =>
		{
			ct.ThrowIfCancellationRequested();
			return await operation();
		}, cancellationToken);
	}

	/// <summary>
	/// Executes an async action with retry logic for transient failures
	/// </summary>
	/// <param name="operation">Operation to execute</param>
	/// <param name="cancellationToken">Cancellation token</param>
	public async Task ExecuteWithRetryAsync(
		Func<Task> operation,
		CancellationToken cancellationToken = default)
	{
		await _retryPipeline.ExecuteAsync(async (ct) =>
		{
			ct.ThrowIfCancellationRequested();
			await operation();
		}, cancellationToken);
	}

	#endregion

	#region Test Data Cleanup

	/// <summary>
	/// Registers a cleanup action to be executed when the manager is disposed
	/// </summary>
	/// <param name="cleanupAction">Cleanup action to register</param>
	public void RegisterCleanupAction(Action cleanupAction)
	{
		if (cleanupAction == null)
		{
			throw new ArgumentNullException(nameof(cleanupAction));
		}

		_cleanupActions.Add(cleanupAction);
		_logger?.LogDebug("Registered cleanup action. Total cleanup actions: {Count}", _cleanupActions.Count);
	}

	/// <summary>
	/// Executes all registered cleanup actions
	/// </summary>
	public void ExecuteCleanup()
	{
		_logger?.LogInformation("Executing {Count} cleanup actions", _cleanupActions.Count);

		foreach (var action in _cleanupActions)
		{
			try
			{
				action();
			}
			catch (Exception ex)
			{
				_logger?.LogError(ex, "Error executing cleanup action: {Message}", ex.Message);
			}
		}

		_cleanupActions.Clear();
		_logger?.LogInformation("Cleanup completed");
	}

	#endregion

	#region Helper Methods

	/// <summary>
	/// Waits for a repository to be analyzed (with timeout)
	/// </summary>
	/// <param name="maxWaitTime">Maximum time to wait</param>
	/// <param name="pollingInterval">Interval between checks</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>True if repository was analyzed within timeout, false otherwise</returns>
	public async Task<bool> WaitForRepositoryAnalysisAsync(
		TimeSpan? maxWaitTime = null,
		TimeSpan? pollingInterval = null,
		CancellationToken cancellationToken = default)
	{
		var maxWait = maxWaitTime ?? TimeSpan.FromMinutes(5);
		var interval = pollingInterval ?? TimeSpan.FromSeconds(10);
		var startTime = DateTime.UtcNow;

		_logger?.LogInformation(
			"Waiting for repository analysis (max {MaxWait}s, polling every {Interval}s)",
			maxWait.TotalSeconds, interval.TotalSeconds);

		while (DateTime.UtcNow - startTime < maxWait)
		{
			if (await VerifyRepositoryAnalyzedAsync(cancellationToken))
			{
				_logger?.LogInformation(
					"Repository analysis completed after {Elapsed}s",
					(DateTime.UtcNow - startTime).TotalSeconds);
				return true;
			}

			await Task.Delay(interval, cancellationToken);
		}

		_logger?.LogWarning(
			"Repository analysis did not complete within {MaxWait}s",
			maxWait.TotalSeconds);
		return false;
	}

	/// <summary>
	/// Gets comprehensive test environment status
	/// </summary>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Environment status information</returns>
	public async Task<TestEnvironmentStatus> GetEnvironmentStatusAsync(
		CancellationToken cancellationToken = default)
	{
		var status = new TestEnvironmentStatus
		{
			Organization = _testOrganization,
			Repository = _testRepository,
			Provider = _testProvider
		};

		try
		{
			status.RepositoryExists = await VerifyRepositoryExistsAsync(cancellationToken);

			if (status.RepositoryExists)
			{
				status.HasAnalysisData = await VerifyRepositoryAnalyzedAsync(cancellationToken);
				status.HasBranches = await VerifyRepositoryHasBranchesAsync(cancellationToken);

				var branches = await GetTestRepositoryBranchesAsync(cancellationToken: cancellationToken);
				status.BranchCount = branches.Count;

				var files = await GetTestRepositoryFilesAsync(cancellationToken: cancellationToken);
				status.FileCount = files.Count;
			}
		}
		catch (Exception ex)
		{
			_logger?.LogError(ex, "Error getting environment status: {Message}", ex.Message);
			status.ErrorMessage = ex.Message;
		}

		return status;
	}

	#endregion

	#region IDisposable

	/// <summary>
	/// Disposes the TestDataManager and executes cleanup actions
	/// </summary>
	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	/// <summary>
	/// Disposes the TestDataManager
	/// </summary>
	/// <param name="disposing">True if disposing, false if finalizing</param>
	protected virtual void Dispose(bool disposing)
	{
		if (!_disposed)
		{
			if (disposing)
			{
				// Execute cleanup actions
				ExecuteCleanup();

				// Dispose the client if we own it
				_client?.Dispose();
			}

			_disposed = true;
		}
	}

	#endregion
}

/// <summary>
/// Test environment status information
/// </summary>
public class TestEnvironmentStatus
{
	/// <summary>Organization name</summary>
	public required string Organization { get; init; }

	/// <summary>Repository name</summary>
	public required string Repository { get; init; }

	/// <summary>Provider</summary>
	public required Provider Provider { get; init; }

	/// <summary>Whether the repository exists in Codacy</summary>
	public bool RepositoryExists { get; set; }

	/// <summary>Whether the repository has analysis data</summary>
	public bool HasAnalysisData { get; set; }

	/// <summary>Whether the repository has branches</summary>
	public bool HasBranches { get; set; }

	/// <summary>Number of branches</summary>
	public int BranchCount { get; set; }

	/// <summary>Number of files</summary>
	public int FileCount { get; set; }

	/// <summary>Error message if status check failed</summary>
	public string? ErrorMessage { get; set; }

	/// <summary>Whether the environment is ready for testing</summary>
	public bool IsReady => RepositoryExists && HasAnalysisData && HasBranches;

	/// <summary>Gets a summary of the environment status</summary>
	public override string ToString()
	{
		if (!string.IsNullOrEmpty(ErrorMessage))
		{
			return $"Error: {ErrorMessage}";
		}

		return $"Repository: {Provider}/{Organization}/{Repository} | " +
			   $"Exists: {RepositoryExists} | " +
			   $"Analyzed: {HasAnalysisData} | " +
			   $"Branches: {BranchCount} | " +
			   $"Files: {FileCount} | " +
			   $"Ready: {IsReady}";
	}
}

