namespace Codacy.Api.Models;

/// <summary>
/// A change skipped due to a conflict with an existing coding standard
/// </summary>
public class AutoconfigConflict
{
	/// <summary>Pattern identifier</summary>
	public string? PatternId { get; set; }

	/// <summary>Name of the tool the pattern belongs to</summary>
	public required string ToolName { get; set; }

	/// <summary>Conflict type</summary>
	public required string Conflict { get; set; }

	/// <summary>Name of the coding standard that caused the conflict</summary>
	public string? CodingStandardName { get; set; }

	/// <summary>Explanation of why the change was skipped</summary>
	public required string Reason { get; set; }
}
