namespace Codacy.Api.Models;

/// <summary>
/// An issue type of a Jira project
/// </summary>
public class JiraProjectIssueType
{
	/// <summary>Issue type identifier</summary>
	public required long Id { get; set; }

	/// <summary>Issue type name</summary>
	public required string Name { get; set; }

	/// <summary>True if the issue type is a subtask</summary>
	public required bool IsSubtask { get; set; }

	/// <summary>Icon URL</summary>
	public string? IconUrl { get; set; }
}
