using Codacy.Api.Models;
using Refit;

namespace Codacy.Api.Interfaces;

/// <summary>
/// Interface for Analysis API operations
/// </summary>
public interface IAnalysisApi
{
	/// <summary>
	/// List organization repositories with analysis information
	/// </summary>
	[Get("/api/v3/analysis/organizations/{provider}/{organizationName}/repositories")]
	Task<RepositoryListResponse> ListOrganizationRepositoriesWithAnalysisAsync(
		Provider provider,
		string organizationName,
		[Query] string? cursor = null,
		[Query] int? limit = null,
		[Query] string? search = null,
		[Query] string? repositories = null,
		[Query] string? segments = null,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Search organization repositories with analysis information
	/// </summary>
	[Post("/api/v3/search/analysis/organizations/{provider}/{organizationName}/repositories")]
	Task<RepositoryListResponse> SearchOrganizationRepositoriesWithAnalysisAsync(
		Provider provider,
		string organizationName,
		[Body] SearchOrganizationRepositoriesRequest body,
		[Query] string? cursor = null,
		[Query] int? limit = null,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Get a repository with analysis information
	/// </summary>
	[Get("/api/v3/analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}")]
	Task<RepositoryWithAnalysisResponse> GetRepositoryWithAnalysisAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		[Query] string? branch = null,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// List repository tools
	/// </summary>
	[Get("/api/v3/analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/tools")]
	Task<AnalysisToolsResponse> ListRepositoryToolsAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// List repository tool conflicts
	/// </summary>
	[Get("/api/v3/analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/tools/conflicts")]
	Task<RepositoryConflictsResponse> ListRepositoryToolConflictsAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Configure a repository tool
	/// </summary>
	[Patch("/api/v3/analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/tools/{toolUuid}")]
	Task ConfigureToolAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		string toolUuid,
		[Body] ConfigureToolBody body,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Get analysis progress
	/// </summary>
	[Get("/api/v3/analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/analysis-progress")]
	Task<FirstAnalysisOverviewResponse> GetFirstAnalysisOverviewAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		[Query] string? branch = null,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// List pull requests from a repository
	/// </summary>
	[Get("/api/v3/analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/pull-requests")]
	Task<PullRequestWithAnalysisListResponse> ListRepositoryPullRequestsAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		[Query] int? limit = null,
		[Query] string? cursor = null,
		[Query] string? search = null,
		[Query] bool includeNotAnalyzed = false,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Get a specific pull request
	/// </summary>
	[Get("/api/v3/analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/pull-requests/{pullRequestNumber}")]
	Task<PullRequestWithAnalysis> GetRepositoryPullRequestAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		int pullRequestNumber,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Get pull request commits
	/// </summary>
	[Get("/api/v3/analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/pull-requests/{pullRequestNumber}/commits")]
	Task<CommitWithAnalysisListResponse> GetPullRequestCommitsAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		int pullRequestNumber,
		[Query] int? limit = null,
		[Query] string? cursor = null,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Bypass pull request analysis
	/// </summary>
	[Post("/api/v3/analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/pull-requests/{pullRequestNumber}/bypass")]
	Task BypassPullRequestAnalysisAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		int pullRequestNumber,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Get pull request issues
	/// </summary>
	[Get("/api/v3/analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/pull-requests/{pullRequestNumber}/issues")]
	Task<PullRequestIssuesResponse> ListPullRequestIssuesAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		int pullRequestNumber,
		[Query] string? status = null,
		[Query] bool? onlyPotential = null,
		[Query] string? cursor = null,
		[Query] int? limit = null,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Get pull request clones
	/// </summary>
	[Get("/api/v3/analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/pull-requests/{pullRequestNumber}/clones")]
	Task<ClonesResponse> ListPullRequestClonesAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		int pullRequestNumber,
		[Query] string? status = null,
		[Query] bool? onlyPotential = null,
		[Query] string? cursor = null,
		[Query] int? limit = null,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Get pull request logs
	/// </summary>
	[Get("/api/v3/analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/pull-requests/{pullRequestNumber}/logs")]
	Task<LogsResponse> ListPullRequestLogsAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		int pullRequestNumber,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// List commit analysis statistics
	/// </summary>
	[Get("/api/v3/analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/commit-statistics")]
	Task<CommitAnalysisStatsListResponse> ListCommitAnalysisStatsAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		[Query] string? branch = null,
		[Query] int days = 31,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// List category overviews
	/// </summary>
	[Get("/api/v3/analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/category-overviews")]
	Task<CategoryOverviewListResponse> ListCategoryOverviewsAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		[Query] string? branch = null,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Search repository issues
	/// </summary>
	[Post("/api/v3/analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/issues/search")]
	Task<SearchRepositoryIssuesListResponse> SearchRepositoryIssuesAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		[Body] SearchRepositoryIssuesBody? body = null,
		[Query] string? cursor = null,
		[Query] int? limit = null,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Get issues overview
	/// </summary>
	[Post("/api/v3/analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/issues/overview")]
	Task<IssuesOverviewResponse> GetIssuesOverviewAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		[Body] SearchRepositoryIssuesBody? body = null,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Get an issue
	/// </summary>
	[Get("/api/v3/analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/issues/{issueId}")]
	Task<GetIssueResponse> GetIssueAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		long issueId,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Update issue state (ignore/unignore)
	/// </summary>
	[Patch("/api/v3/analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/issues/{issueId}")]
	Task UpdateIssueStateAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		string issueId,
		[Body] IssueStateBody body,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Bulk ignore issues
	/// </summary>
	[Post("/api/v3/analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/issues/bulk-ignore")]
	Task BulkIgnoreIssuesAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		[Body] BulkIgnoreIssuesBody body,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// List repository commits
	/// </summary>
	[Get("/api/v3/analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/commits")]
	Task<CommitWithAnalysisListResponse> ListRepositoryCommitsAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		[Query] string? branch = null,
		[Query] string? cursor = null,
		[Query] int? limit = null,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Get a specific commit
	/// </summary>
	[Get("/api/v3/analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/commits/{commitUuid}")]
	Task<CommitWithAnalysis> GetCommitAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		string commitUuid,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Get commit delta statistics
	/// </summary>
	[Get("/api/v3/analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/commits/{commitUuid}/deltaStatistics")]
	Task<CommitDeltaStatistics> GetCommitDeltaStatisticsAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		string commitUuid,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Get commit delta issues
	/// </summary>
	[Get("/api/v3/analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/commits/{srcCommitUuid}/deltaIssues")]
	Task<CommitDeltaIssuesResponse> ListCommitDeltaIssuesAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		string srcCommitUuid,
		[Query] string? targetCommitUuid = null,
		[Query] string? status = null,
		[Query] bool? onlyPotential = null,
		[Query] string? cursor = null,
		[Query] int? limit = null,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Get commit clones
	/// </summary>
	[Get("/api/v3/analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/commits/{commitUuid}/clones")]
	Task<ClonesResponse> ListCommitClonesAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		string commitUuid,
		[Query] string? status = null,
		[Query] bool? onlyPotential = null,
		[Query] string? cursor = null,
		[Query] int? limit = null,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Get commit logs
	/// </summary>
	[Get("/api/v3/analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/commits/{commitUuid}/logs")]
	Task<LogsResponse> ListCommitLogsAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		string commitUuid,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Get commit files
	/// </summary>
	[Get("/api/v3/analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/commits/{commitUuid}/files")]
	Task<FileAnalysisListResponse> ListCommitFilesAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		string commitUuid,
		[Query] string? branch = null,
		[Query] string? filter = null,
		[Query] string? cursor = null,
		[Query] int? limit = null,
		[Query] string? search = null,
		[Query] string? sortColumn = null,
		[Query] string? columnOrder = null,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Get pull request files
	/// </summary>
	[Get("/api/v3/analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/pull-requests/{pullRequestNumber}/files")]
	Task<FileAnalysisListResponse> ListPullRequestFilesAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		int pullRequestNumber,
		[Query] string? cursor = null,
		[Query] int? limit = null,
		[Query] string? sortColumn = null,
		[Query] string? columnOrder = null,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// List organization pull requests
	/// </summary>
	[Get("/api/v3/analysis/organizations/{provider}/{organizationName}/pull-requests")]
	Task<PullRequestWithAnalysisListResponse> ListOrganizationPullRequestsAsync(
		Provider provider,
		string organizationName,
		[Query] int? limit = null,
		[Query] string? search = null,
		[Query] string? repositories = null,
		CancellationToken cancellationToken = default);
}
