namespace Codacy.Api.Models;

/// <summary>
/// Common filters for AI inventory queries
/// </summary>
public class AiInventoryFilter
{
	/// <summary>Inventory item types to filter by (for example tool, asset)</summary>
	public List<string>? InventoryItemTypes { get; set; }

	/// <summary>AI provider name to filter by</summary>
	public string? AiProvider { get; set; }

	/// <summary>Segment IDs to filter by</summary>
	public List<long>? Segments { get; set; }

	/// <summary>Repository names to filter by</summary>
	public List<string>? Repositories { get; set; }

	/// <summary>Inventory category groups to filter by (for example workflows, usage, mcps)</summary>
	public List<string>? CategoryGroups { get; set; }

	/// <summary>Inventory categories to filter by (for example code_marker, commits)</summary>
	public List<string>? Categories { get; set; }

	/// <summary>Marker text to filter by</summary>
	public string? Marker { get; set; }
}
