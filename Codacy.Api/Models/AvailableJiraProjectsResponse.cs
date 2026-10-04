namespace Codacy.Api.Models;

/// <summary>
/// A response with a list of available Jira projects
/// </summary>
public class AvailableJiraProjectsResponse
{
	/// <summary>The projects</summary>
	public required List<JiraProject> Data { get; set; }

	/// <summary>Pagination info</summary>
	public PaginationInfoLong? Pagination { get; set; }
}
