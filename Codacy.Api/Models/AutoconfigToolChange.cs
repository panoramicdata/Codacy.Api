namespace Codacy.Api.Models;

/// <summary>
/// A tool that was enabled or disabled by an autoconfig run
/// </summary>
public class AutoconfigToolChange
{
	/// <summary>Name of the tool</summary>
	public required string ToolName { get; set; }

	/// <summary>Whether the tool was enabled or disabled</summary>
	public required string Action { get; set; }

	/// <summary>Reason for the change</summary>
	public required string Reason { get; set; }

	/// <summary>Number of patterns affected by this tool change</summary>
	public required int PatternsAffected { get; set; }
}
