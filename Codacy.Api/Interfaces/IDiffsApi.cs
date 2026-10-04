using Codacy.Api.Models;
using Refit;

namespace Codacy.Api.Interfaces;

/// <summary>
/// Interface for diff and commit detail API operations
/// </summary>
public interface IDiffsApi
{
	/// <summary>
	/// Get the diff of a pull request
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/repositories/{repositoryName}/pull-requests/{pullRequestNumber}/diff")]
	Task<DiffResponse> GetPullRequestDiffAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		int pullRequestNumber,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get the diff of a commit
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/repositories/{repositoryName}/commits/{commitUuid}/diff")]
	Task<DiffResponse> GetCommitDiffAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		string commitUuid,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get the diff between two commits
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/repositories/{repositoryName}/base/{baseCommitUuid}/head/{headCommitUuid}/diff")]
	Task<DiffResponse> GetDiffBetweenCommitsAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		string baseCommitUuid,
		string headCommitUuid,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get the details of a commit by its identifier
	/// </summary>
	[Get("/api/v3/commits/{commitId}")]
	Task<CommitDetailsV2> GetCommitDetailsByCommitIdAsync(
		long commitId,
		CancellationToken cancellationToken);
}
