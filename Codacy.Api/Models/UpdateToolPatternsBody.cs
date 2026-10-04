namespace Codacy.Api.Models;

/// <summary>
/// Specifies the update to apply to the code patterns of a tool in a repository
/// </summary>
public class UpdateToolPatternsBody
{
	/// <summary>True enables the code patterns, and false disables them</summary>
	public required bool Enabled { get; set; }
}
