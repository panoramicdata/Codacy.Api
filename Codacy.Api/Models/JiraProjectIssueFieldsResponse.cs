namespace Codacy.Api.Models;

/// <summary>
/// A response with a list of possible fields for a Jira project issue type
/// </summary>
public class JiraProjectIssueFieldsResponse
{
	/// <summary>The fields</summary>
	public required List<JiraProjectIssueField> Data { get; set; }

	/// <summary>Pagination info</summary>
	public PaginationInfoLong? Pagination { get; set; }
}
