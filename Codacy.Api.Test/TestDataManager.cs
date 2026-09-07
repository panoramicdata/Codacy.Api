#pragma warning disable CA1848 // Use LoggerMessage delegates for improved performance
#pragma warning disable CA1873 // Test diagnostics favor readability over deferred argument evaluation
#pragma warning disable S2360 // Optional parameters should not be used - This is a test helper class where optional parameters improve usability

using Microsoft.Extensions.Logging.Abstractions;

using Polly;
using Refit;
using System.Net;

namespace Codacy.Api.Test;

/// <summary>
/// Manages test data lifecycle for integration tests, including seeding, cleanup, and retry logic
/// </summary>
public sealed class TestDataManager : IDisposable
{
	private readonly CodacyClient _client;
	private readonly ILogger _logger;
	private readonly ResiliencePipeline _retryPipeline;
	private readonly TestCleanupRegistry _cleanupRegistry;
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
		ArgumentNullException.ThrowIfNull(client);
		ArgumentNullException.ThrowIfNull(testOrganization);
		ArgumentNullException.ThrowIfNull(testRepository);

		_client = client;
		_testOrganization = testOrganization;
		_testRepository = testRepository;
		_testProvider = testProvider;
		_logger = logger ?? NullLogger.Instance;
		_cleanupRegistry = new TestCleanupRegistry(logger);
		_retryPipeline = TestRetryPipelineFactory.Create(logger, maxRetries);
	}

	#region Test Data Verification

	/// <summary>
	/// Verifies that the test repository exists in Codacy
	/// </summary>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>True if repository exists, false otherwise</returns>
	public Task<bool> VerifyRepositoryExistsAsync(CancellationToken cancellationToken = default)
		=> VerifyAsync(
			"exists in Codacy",
			async () => await GetRepositoryOrNullAsync(cancellationToken) is not null,
			cancellationToken,
			HttpStatusCode.NotFound);

	/// <summary>
	/// Verifies that the test repository has been analyzed
	/// </summary>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>True if repository has analysis data, false otherwise</returns>
	public Task<bool> VerifyRepositoryAnalyzedAsync(CancellationToken cancellationToken = default)
		=> VerifyAsync(
			"has analysis data",
			async () => (await ListFilesAsync(null, null, cancellationToken)).Data.Count > 0,
			cancellationToken,
			HttpStatusCode.NotFound);

	/// <summary>
	/// Verifies that the test repository has branches
	/// </summary>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>True if repository has branches, false otherwise</returns>
	public Task<bool> VerifyRepositoryHasBranchesAsync(CancellationToken cancellationToken = default)
		=> VerifyAsync(
			"has branches",
			async () => (await ListBranchesAsync(null, cancellationToken)).Data.Count > 0,
			cancellationToken,
			HttpStatusCode.NotFound,
			HttpStatusCode.BadRequest);

	#endregion

	#region Test Data Retrieval

	/// <summary>
	/// Gets the test repository details
	/// </summary>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>Repository details</returns>
	public Task<Repository?> GetTestRepositoryAsync(CancellationToken cancellationToken = default)
		=> TryGetAsync(
			"details",
			() => GetRepositoryOrNullAsync(cancellationToken),
			whenUnavailable: null,
			cancellationToken,
			HttpStatusCode.NotFound);

	/// <summary>
	/// Gets a list of branches for the test repository
	/// </summary>
	/// <param name="limit">Maximum number of branches to retrieve</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <returns>List of branches</returns>
	public Task<List<Branch>> GetTestRepositoryBranchesAsync(
		int? limit = null,
		CancellationToken cancellationToken = default)
		=> TryGetAsync(
			"branches",
			async () => (await ListBranchesAsync(limit, cancellationToken)).Data,
			whenUnavailable: [],
			cancellationToken,
			HttpStatusCode.NotFound,
			HttpStatusCode.BadRequest);

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
		=> TryGetAsync(
			"files",
			async () => (await ListFilesAsync(branch, limit, cancellationToken)).Data,
			whenUnavailable: [],
			cancellationToken,
			HttpStatusCode.NotFound);

	private async Task<Repository?> GetRepositoryOrNullAsync(CancellationToken cancellationToken)
	{
		var response = await _client.Repositories.GetRepositoryAsync(
			_testProvider,
			_testOrganization,
			_testRepository,
			cancellationToken);

		return response.Data;
	}

	private Task<BranchListResponse> ListBranchesAsync(int? limit, CancellationToken cancellationToken)
		=> _client.Repositories.ListRepositoryBranchesAsync(
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

	private Task<FileListResponse> ListFilesAsync(string? branch, int? limit, CancellationToken cancellationToken)
		=> _client.Repositories.ListFilesAsync(
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

	/// <summary>
	/// Runs an operation against the test repository with retry, reading the tolerated statuses
	/// as "the test environment does not have this yet" rather than as a failure.
	/// </summary>
	/// <typeparam name="T">Return type</typeparam>
	/// <param name="what">What is being fetched, for the diagnostic message</param>
	/// <param name="operation">Operation to execute</param>
	/// <param name="whenUnavailable">Value to return when a tolerated status comes back</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <param name="tolerated">Statuses that mean "not available" rather than "failed"</param>
	private async Task<T> TryGetAsync<T>(
		string what,
		Func<Task<T>> operation,
		T whenUnavailable,
		CancellationToken cancellationToken,
		params HttpStatusCode[] tolerated)
	{
		try
		{
			return await ExecuteWithRetryAsync(operation, cancellationToken);
		}
		catch (ApiException ex) when (tolerated.Contains(ex.StatusCode))
		{
			_logger.LogWarning(
				"Repository {Organization}/{Repository} {What} not available ({Status}): {Message}",
				_testOrganization,
				_testRepository,
				what,
				ex.StatusCode,
				ex.Message);

			return whenUnavailable;
		}
	}

	/// <summary>
	/// Runs a check against the test repository and logs its outcome, reading a tolerated status
	/// as a failed check rather than as an error.
	/// </summary>
	/// <param name="expectation">What is being checked, for the diagnostic message</param>
	/// <param name="check">The check to run</param>
	/// <param name="cancellationToken">Cancellation token</param>
	/// <param name="tolerated">Statuses that mean the check failed rather than errored</param>
	private async Task<bool> VerifyAsync(
		string expectation,
		Func<Task<bool>> check,
		CancellationToken cancellationToken,
		params HttpStatusCode[] tolerated)
	{
		var satisfied = await TryGetAsync(expectation, check, false, cancellationToken, tolerated);

		_logger.Log(
			satisfied ? LogLevel.Information : LogLevel.Warning,
			"Repository {Organization}/{Repository} {Expectation}: {Satisfied}",
			_testOrganization,
			_testRepository,
			expectation,
			satisfied);

		return satisfied;
	}

	#endregion

	#region Test Data Cleanup

	/// <summary>
	/// Registers a cleanup action to be executed when the manager is disposed
	/// </summary>
	/// <param name="cleanupAction">Cleanup action to register</param>
	public void RegisterCleanupAction(Action cleanupAction)
		=> _cleanupRegistry.Register(cleanupAction);

	/// <summary>
	/// Executes all registered cleanup actions
	/// </summary>
	public void ExecuteCleanup()
		=> _cleanupRegistry.Execute();

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

		_logger.LogInformation(
			"Waiting for repository analysis (max {MaxWait}s, polling every {Interval}s)",
			maxWait.TotalSeconds,
			interval.TotalSeconds);

		while (DateTime.UtcNow - startTime < maxWait)
		{
			if (await VerifyRepositoryAnalyzedAsync(cancellationToken))
			{
				_logger.LogInformation(
					"Repository analysis completed after {Elapsed}s",
					(DateTime.UtcNow - startTime).TotalSeconds);
				return true;
			}

			await Task.Delay(interval, cancellationToken);
		}

		_logger.LogWarning(
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
				await PopulateAnalysisStatusAsync(status, cancellationToken);
			}
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error getting environment status: {Message}", ex.Message);
			status.ErrorMessage = ex.Message;
		}

		return status;
	}

	private async Task PopulateAnalysisStatusAsync(
		TestEnvironmentStatus status,
		CancellationToken cancellationToken)
	{
		status.HasAnalysisData = await VerifyRepositoryAnalyzedAsync(cancellationToken);
		status.HasBranches = await VerifyRepositoryHasBranchesAsync(cancellationToken);
		status.BranchCount = (await GetTestRepositoryBranchesAsync(cancellationToken: cancellationToken)).Count;
		status.FileCount = (await GetTestRepositoryFilesAsync(cancellationToken: cancellationToken)).Count;
	}

	#endregion

	#region IDisposable

	/// <summary>
	/// Disposes the TestDataManager, executing cleanup actions and disposing the client
	/// </summary>
	public void Dispose()
	{
		if (_disposed)
		{
			return;
		}

		ExecuteCleanup();
		_client.Dispose();
		_disposed = true;
	}

	#endregion
}
