using Codacy.Api.Models;
using Refit;

namespace Codacy.Api.Interfaces;

/// <summary>
/// Interface for Coverage API operations
/// </summary>
public interface ICoverageApi
{
	/// <summary>
	/// Get pull request coverage
	/// </summary>
	[Get("/api/v3/coverage/organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/pull-requests/{pullRequestNumber}")]
	Task<PullRequestWithCoverageResponse> GetRepositoryPullRequestCoverageAsync(
		Provider provider,
		string remoteOrganizationName,
		string repositoryName,
		int pullRequestNumber,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get pull request files coverage
	/// </summary>
	[Get("/api/v3/coverage/organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/pull-requests/{pullRequestNumber}/files")]
	Task<PullRequestFilesCoverageResponse> GetRepositoryPullRequestFilesCoverageAsync(
		Provider provider,
		string remoteOrganizationName,
		string repositoryName,
		int pullRequestNumber,
		CancellationToken cancellationToken);

	/// <summary>
	/// Reanalyze coverage for pull request
	/// </summary>
	[Get("/api/v3/coverage/organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/pull-requests/{pullRequestNumber}/reanalyze")]
	Task ReanalyzeCoverageAsync(
		Provider provider,
		string remoteOrganizationName,
		string repositoryName,
		int pullRequestNumber,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get pull request coverage reports status
	/// </summary>
	[Get("/api/v3/analysis/organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/pull-requests/{pullRequestNumber}/coverage/status")]
	Task<CoveragePullRequestResponse> GetPullRequestCoverageReportsAsync(
		Provider provider,
		string remoteOrganizationName,
		string repositoryName,
		int pullRequestNumber,
		CancellationToken cancellationToken);
}
