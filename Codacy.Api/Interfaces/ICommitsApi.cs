using Codacy.Api.Models;
using Refit;

namespace Codacy.Api.Interfaces;

/// <summary>
/// Interface for Commits API operations
/// Note: Commit operations are primarily handled through IAnalysisApi
/// </summary>
public interface ICommitsApi
{
	// Commit-related operations are available through IAnalysisApi:
	// - ListRepositoryCommitsAsync
	// - GetCommitAsync
	// - GetCommitDeltaStatisticsAsync
	// - ListCommitDeltaIssuesAsync
	// - ListCommitClonesAsync
	// - ListCommitLogsAsync
	// - ListCommitFilesAsync
	// - GetPullRequestCommitsAsync

	/// <summary>
	/// Placeholder method to satisfy Refit interface requirements
	/// </summary>
	[Get("/api/v3/version")]
	Task<VersionResponse> PlaceholderAsync(CancellationToken cancellationToken);
}
