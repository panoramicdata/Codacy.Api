namespace Codacy.Api.Models;

/// <summary>
/// A Jira project
/// </summary>
public class JiraProject
{
	/// <summary>Project identifier</summary>
	public required long Id { get; set; }

	/// <summary>Project key</summary>
	public required string Key { get; set; }

	/// <summary>Project name</summary>
	public required string Name { get; set; }

	/// <summary>Project avatar URL</summary>
	public required string AvatarUrl { get; set; }
}
