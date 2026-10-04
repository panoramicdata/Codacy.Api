namespace Codacy.Api.Models;

/// <summary>
/// A possible field for a Jira project issue type
/// </summary>
public class JiraProjectIssueField
{
	/// <summary>Field identifier</summary>
	public required string FieldId { get; set; }

	/// <summary>Field name</summary>
	public required string Name { get; set; }

	/// <summary>Field key</summary>
	public required string Key { get; set; }

	/// <summary>True if the field is required</summary>
	public required bool Required { get; set; }

	/// <summary>True if the field has a default value</summary>
	public required bool HasDefaultValue { get; set; }
}
