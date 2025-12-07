using Codacy.Api.Models;
using Refit;

namespace Codacy.Api.Interfaces;

/// <summary>
/// Interface for Issues API operations
/// </summary>
public interface IIssuesApi
{
	/// <summary>
	/// Search repository issues
	/// </summary>
	[Post("/api/v3/analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/issues/search")]
	Task<SearchRepositoryIssuesListResponse> SearchRepositoryIssuesAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		[Body] SearchRepositoryIssuesBody? filter,
		[Query] string? cursor,
		[Query] int? limit,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get an issue
	/// </summary>
	[Get("/api/v3/analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/issues/{issueId}")]
	Task<GetIssueResponse> GetIssueAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		long issueId,
		CancellationToken cancellationToken);

	/// <summary>
	/// Update issue state (ignore/unignore)
	/// </summary>
	[Patch("/api/v3/analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/issues/{issueId}")]
	Task UpdateIssueStateAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		string issueId,
		[Body] IssueStateBody state,
		CancellationToken cancellationToken);

	/// <summary>
	/// Bulk ignore issues
	/// </summary>
	[Post("/api/v3/analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/issues/bulk-ignore")]
	Task BulkIgnoreIssuesAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		[Body] BulkIgnoreIssuesBody body,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get issues overview
	/// </summary>
	[Post("/api/v3/analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/issues/overview")]
	Task<IssuesOverviewResponse> GetIssuesOverviewAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		[Body] SearchRepositoryIssuesBody? filter,
		CancellationToken cancellationToken);

	/// <summary>
	/// Search ignored issues
	/// </summary>
	[Post("/api/v3/analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/issues/ignored/search")]
	Task<ListResponse<IgnoredIssue>> SearchRepositoryIgnoredIssuesAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		[Body] SearchRepositoryIssuesBody? filter,
		[Query] string? cursor,
		[Query] int? limit,
		CancellationToken cancellationToken);

	/// <summary>
	/// List pull request issues
	/// </summary>
	[Get("/api/v3/analysis/organizations/{provider}/{organizationName}/repositories/{repositoryName}/pull-requests/{pullRequestNumber}/issues")]
	Task<PullRequestIssuesResponse> ListPullRequestIssuesAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		int pullRequestNumber,
		[Query] string? status,
		[Query] bool onlyPotential,
		[Query] string? cursor,
		[Query] int? limit,
		CancellationToken cancellationToken);
}
