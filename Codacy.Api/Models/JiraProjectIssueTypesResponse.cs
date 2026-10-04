namespace Codacy.Api.Models;

/// <summary>
/// A response with a list of issue types for a Jira project
/// </summary>
public class JiraProjectIssueTypesResponse
{
	/// <summary>The issue types</summary>
	public required List<JiraProjectIssueType> Data { get; set; }

	/// <summary>Pagination info</summary>
	public PaginationInfoLong? Pagination { get; set; }
}
