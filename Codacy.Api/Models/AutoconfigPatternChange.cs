namespace Codacy.Api.Models;

/// <summary>
/// A pattern that was enabled, disabled, or updated by an autoconfig run
/// </summary>
public class AutoconfigPatternChange
{
	/// <summary>Pattern identifier</summary>
	public required string PatternId { get; set; }

	/// <summary>Name of the tool the pattern belongs to</summary>
	public required string ToolName { get; set; }

	/// <summary>Whether the pattern was enabled, disabled, or updated</summary>
	public required string Action { get; set; }

	/// <summary>Reason for the change</summary>
	public required string Reason { get; set; }

	/// <summary>Net change in issue count caused by this pattern change</summary>
	public required int DeltaIssues { get; set; }

	/// <summary>Parameter values that changed</summary>
	public required List<AutoconfigParameterChange> Parameters { get; set; }
}
