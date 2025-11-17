using Codacy.Api.Models;
using Refit;

namespace Codacy.Api.Interfaces;

/// <summary>
/// Interface for Pull Requests API operations
/// Note: Pull request operations are primarily handled through IAnalysisApi
/// </summary>
public interface IPullRequestsApi
{
	// Pull request-related operations are available through IAnalysisApi:
	// - ListRepositoryPullRequestsAsync
	// - GetRepositoryPullRequestAsync
	// - GetPullRequestCommitsAsync
	// - BypassPullRequestAnalysisAsync
	// - ListPullRequestIssuesAsync
	// - ListPullRequestClonesAsync
	// - ListPullRequestLogsAsync
	// - ListPullRequestFilesAsync
	// - ListOrganizationPullRequestsAsync
	
	/// <summary>
	/// Placeholder method to satisfy Refit interface requirements
	/// </summary>
	[Get("/api/v3/version")]
	Task<VersionResponse> PlaceholderAsync(CancellationToken cancellationToken = default);
}
