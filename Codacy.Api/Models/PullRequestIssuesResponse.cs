namespace Codacy.Api.Models;

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
