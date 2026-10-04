namespace Codacy.Api.Models;

/// <summary>
/// Code pattern configuration for a tool
/// </summary>
public class ToolConfiguredPattern
{
	/// <summary>Definition of the code pattern</summary>
	public required Pattern PatternDefinition { get; set; }

	/// <summary>True if the code pattern is enabled</summary>
	public required bool Enabled { get; set; }

	/// <summary>Whether the pattern is enabled outside the scope of a coding standard</summary>
	public required bool IsCustom { get; set; }

	/// <summary>Configured parameters of the code pattern</summary>
	public required List<ConfiguredParameter> Parameters { get; set; }

	/// <summary>Coding standards that enable the pattern</summary>
	public required List<CodingStandardInfo> EnabledBy { get; set; }
}
