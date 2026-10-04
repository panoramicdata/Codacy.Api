namespace Codacy.Api.Models;

/// <summary>
/// Identifies conflicts in a pattern
/// </summary>
public class StandardPatternConflict
{
	/// <summary>The pattern id</summary>
	public required string PatternId { get; set; }

	/// <summary>Conflicts for the pattern</summary>
	public required List<StandardParameterConflict> Conflicts { get; set; }
}
