namespace Codacy.Api.Models;

/// <summary>
/// Summary for a distinct inventory resource (category group, category, marker)
/// </summary>
public class AiInventoryMarkerSummary
{
	/// <summary>Category group the marker belongs to</summary>
	public required string CategoryGroup { get; set; }

	/// <summary>Category the marker belongs to within its group</summary>
	public required string Category { get; set; }

	/// <summary>Marker text identifying the inventory item</summary>
	public required string Marker { get; set; }

	/// <summary>Total number of references for this marker</summary>
	public required int ReferencesCount { get; set; }

	/// <summary>Number of distinct repositories containing this marker</summary>
	public required int RepositoriesCount { get; set; }
}
