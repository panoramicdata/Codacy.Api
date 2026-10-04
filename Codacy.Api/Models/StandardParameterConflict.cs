namespace Codacy.Api.Models;

/// <summary>
/// Coding standard and corresponding pattern parameters that have conflicts
/// </summary>
public class StandardParameterConflict
{
	/// <summary>Coding standard that has the conflict</summary>
	public required CodingStandardInfo Standard { get; set; }

	/// <summary>Conflicting parameters</summary>
	public required List<ConfiguredParameter> Parameters { get; set; }
}
