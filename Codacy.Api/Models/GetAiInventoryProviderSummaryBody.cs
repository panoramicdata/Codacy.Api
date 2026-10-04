namespace Codacy.Api.Models;

/// <summary>
/// Required AI provider identity plus optional filters scoping the returned summary
/// </summary>
public class GetAiInventoryProviderSummaryBody
{
	/// <summary>AI provider name to return the summary for</summary>
	public required string AiProvider { get; set; }

	/// <summary>Inventory item types to filter by</summary>
	public List<string>? InventoryItemTypes { get; set; }

	/// <summary>Segment IDs to filter by</summary>
	public List<long>? Segments { get; set; }

	/// <summary>Repository names to filter by</summary>
	public List<string>? Repositories { get; set; }

	/// <summary>Inventory category groups to filter by</summary>
	public List<string>? CategoryGroups { get; set; }

	/// <summary>Inventory categories to filter by</summary>
	public List<string>? Categories { get; set; }
}
