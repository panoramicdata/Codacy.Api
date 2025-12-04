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
	Task<IgnoredIssuesListResponse> SearchRepositoryIgnoredIssuesAsync(
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

/// <summary>
/// Get issue response
/// </summary>
public class GetIssueResponse
{
	/// <summary>Issue data</summary>
	public required Issue Data { get; set; }
}

/// <summary>
/// Issues overview response
/// </summary>
public class IssuesOverviewResponse
{
	/// <summary>Overview data</summary>
	public required IssuesOverview Data { get; set; }
}

/// <summary>
/// Issues overview
/// </summary>
public class IssuesOverview
{
	/// <summary>Counts</summary>
	public required IssuesOverviewCounts Counts { get; set; }
}

/// <summary>
/// Issues overview counts
/// </summary>
public class IssuesOverviewCounts
{
	/// <summary>Counts by category</summary>
	public required List<Count> Categories { get; set; }

	/// <summary>Counts by language</summary>
	public required List<Count> Languages { get; set; }

	/// <summary>Counts by severity level</summary>
	public required List<Count> Levels { get; set; }

	/// <summary>Counts by tag</summary>
	public required List<Count> Tags { get; set; }

	/// <summary>Counts by pattern</summary>
	public required List<PatternsCount> Patterns { get; set; }

	/// <summary>Counts by author</summary>
	public required List<Count> Authors { get; set; }
}

/// <summary>
/// Count
/// </summary>
public class Count
{
	/// <summary>Name</summary>
	public required string Name { get; set; }

	/// <summary>Total count</summary>
	public required int Total { get; set; }
}

/// <summary>
/// Patterns count
/// </summary>
public class PatternsCount
{
	/// <summary>Pattern ID</summary>
	public required string Id { get; set; }

	/// <summary>Pattern title</summary>
	public required string Title { get; set; }

	/// <summary>Total count</summary>
	public required int Total { get; set; }
}

/// <summary>
/// Ignored issues list response
/// </summary>
public class IgnoredIssuesListResponse
{
	/// <summary>Pagination info</summary>
	public PaginationInfo? Pagination { get; set; }

	/// <summary>Ignored issues</summary>
	public required List<IgnoredIssue> Data { get; set; }
}

/// <summary>
/// Ignored issue
/// </summary>
public class IgnoredIssue
{
	/// <summary>Issue ID</summary>
	public required string IssueId { get; set; }

	/// <summary>Ignore reason</summary>
	public string? Reason { get; set; }

	/// <summary>Comment</summary>
	public string? Comment { get; set; }

	/// <summary>Ignored by name</summary>
	public string? IgnoredByName { get; set; }

	/// <summary>Ignored timestamp</summary>
	public required DateTimeOffset IgnoredTimestamp { get; set; }

	/// <summary>File path</summary>
	public required string FilePath { get; set; }

	/// <summary>File ID</summary>
	public long? FileId { get; set; }

	/// <summary>Pattern info</summary>
	public required PatternDetails PatternInfo { get; set; }

	/// <summary>Tool info</summary>
	public required ToolReference ToolInfo { get; set; }

	/// <summary>Line number</summary>
	public long? LineNumber { get; set; }

	/// <summary>Message</summary>
	public required string Message { get; set; }

	/// <summary>Language</summary>
	public required string Language { get; set; }

	/// <summary>Line text</summary>
	public required string LineText { get; set; }

	/// <summary>Commit info</summary>
	public CommitReference? CommitInfo { get; set; }

	/// <summary>False positive probability</summary>
	public int? FalsePositiveProbability { get; set; }

	/// <summary>False positive reason</summary>
	public string? FalsePositiveReason { get; set; }

	/// <summary>False positive threshold</summary>
	public int? FalsePositiveThreshold { get; set; }
}

/// <summary>
/// Pull request issues response
/// </summary>
public class PullRequestIssuesResponse
{
	/// <summary>Is analyzed</summary>
	public required bool Analyzed { get; set; }

	/// <summary>Pagination info</summary>
	public PaginationInfo? Pagination { get; set; }

	/// <summary>Issues</summary>
	public required List<CommitDeltaIssue> Data { get; set; }
}

/// <summary>
/// Commit delta issue
/// </summary>
public class CommitDeltaIssue
{
	/// <summary>Commit issue</summary>
	public required Issue CommitIssue { get; set; }

	/// <summary>Delta type (Added/Fixed)</summary>
	public required DeltaType DeltaType { get; set; }
}

/// <summary>
/// Delta type
/// </summary>
public enum DeltaType
{
	/// <summary>Issue was added</summary>
	Added,
	/// <summary>Issue was fixed</summary>
	Fixed
}
