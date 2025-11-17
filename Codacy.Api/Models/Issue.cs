namespace Codacy.Api.Models;

/// <summary>
/// Issue information
/// </summary>
public class Issue
{
	/// <summary>Issue ID</summary>
	public required string IssueId { get; set; }

	/// <summary>Stable result data ID</summary>
	public required long ResultDataId { get; set; }

	/// <summary>File path</summary>
	public required string FilePath { get; set; }

	/// <summary>File ID</summary>
	public required long FileId { get; set; }

	/// <summary>Pattern information</summary>
	public required PatternDetails PatternInfo { get; set; }

	/// <summary>Tool information</summary>
	public required ToolReference ToolInfo { get; set; }

	/// <summary>Line number</summary>
	public required long LineNumber { get; set; }

	/// <summary>Issue message</summary>
	public required string Message { get; set; }

	/// <summary>Suggestion</summary>
	public string? Suggestion { get; set; }

	/// <summary>Programming language</summary>
	public required string Language { get; set; }

	/// <summary>Line text content</summary>
	public required string LineText { get; set; }

	/// <summary>Commit information</summary>
	public CommitReference? CommitInfo { get; set; }

	/// <summary>False positive probability</summary>
	public int? FalsePositiveProbability { get; set; }

	/// <summary>False positive reason</summary>
	public string? FalsePositiveReason { get; set; }

	/// <summary>False positive threshold</summary>
	public required int FalsePositiveThreshold { get; set; }
}

/// <summary>
/// Pattern details
/// </summary>
public class PatternDetails
{
	/// <summary>Pattern ID</summary>
	public required string Id { get; set; }

	/// <summary>Pattern title</summary>
	public string? Title { get; set; }

	/// <summary>Category</summary>
	public required string Category { get; set; }

	/// <summary>Sub-category</summary>
	public string? SubCategory { get; set; }

	/// <summary>Severity level</summary>
	public required SeverityLevel SeverityLevel { get; set; }
}

/// <summary>
/// Tool reference
/// </summary>
public class ToolReference
{
	/// <summary>Tool UUID</summary>
	public required string Uuid { get; set; }

	/// <summary>Tool name</summary>
	public required string Name { get; set; }
}

/// <summary>
/// Commit reference
/// </summary>
public class CommitReference
{
	/// <summary>Commit SHA</summary>
	public required string Sha { get; set; }

	/// <summary>Committer email</summary>
	public string? Commiter { get; set; }

	/// <summary>Committer name</summary>
	public string? CommiterName { get; set; }

	/// <summary>Commit timestamp</summary>
	public DateTimeOffset? Timestamp { get; set; }
}

/// <summary>
/// Search repository issues request
/// </summary>
public class SearchRepositoryIssuesBody
{
	/// <summary>Branch name</summary>
	public string? BranchName { get; set; }

	/// <summary>Pattern IDs to filter</summary>
	public List<string>? PatternIds { get; set; }

	/// <summary>Languages to filter</summary>
	public List<string>? Languages { get; set; }

	/// <summary>Categories to filter</summary>
	public List<string>? Categories { get; set; }

	/// <summary>Severity levels to filter</summary>
	public List<SeverityLevel>? Levels { get; set; }

	/// <summary>Tags to filter</summary>
	public List<string>? Tags { get; set; }

	/// <summary>Author emails to filter</summary>
	public List<string>? AuthorEmails { get; set; }
}

/// <summary>
/// Search repository issues response
/// </summary>
public class SearchRepositoryIssuesListResponse
{
	/// <summary>Pagination info</summary>
	public PaginationInfo? Pagination { get; set; }

	/// <summary>Issues</summary>
	public required List<Issue> Data { get; set; }
}

/// <summary>
/// Issue state update
/// </summary>
public class IssueStateBody
{
	/// <summary>Is ignored</summary>
	public required bool Ignored { get; set; }

	/// <summary>Reason for ignoring</summary>
	public string? Reason { get; set; }

	/// <summary>Comment</summary>
	public string? Comment { get; set; }
}

/// <summary>
/// Bulk ignore issues request
/// </summary>
public class BulkIgnoreIssuesBody
{
	/// <summary>Issue IDs to ignore</summary>
	public required List<string> IssueIds { get; set; }

	/// <summary>Reason for ignoring</summary>
	public string? Reason { get; set; }

	/// <summary>Comment</summary>
	public string? Comment { get; set; }
}
