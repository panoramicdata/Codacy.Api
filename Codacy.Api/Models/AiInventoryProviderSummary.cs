namespace Codacy.Api.Models;

/// <summary>
/// Summary of AI inventory for a provider
/// </summary>
public class AiInventoryProviderSummary
{
	/// <summary>AI provider name</summary>
	public required string AiProvider { get; set; }

	/// <summary>Number of distinct inventory resources for this provider</summary>
	public required int ResourcesCount { get; set; }

	/// <summary>Total number of inventory item references for this provider</summary>
	public required int ReferencesCount { get; set; }

	/// <summary>Number of distinct repositories where items for this provider occur</summary>
	public required int RepositoriesCount { get; set; }

	/// <summary>Per-category-group repository counts</summary>
	public required List<CategoryGroupCount> CategoryGroupBreakdown { get; set; }
}
