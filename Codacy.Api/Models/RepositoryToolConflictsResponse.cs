namespace Codacy.Api.Models;

/// <summary>
/// Repository tool patterns that have conflicts
/// </summary>
public class RepositoryToolConflictsResponse
{
	/// <summary>Patterns with conflicts</summary>
	public required List<StandardPatternConflict> Data { get; set; }
}
