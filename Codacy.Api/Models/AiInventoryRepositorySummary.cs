namespace Codacy.Api.Models;

/// <summary>
/// Summary of AI inventory for a repository
/// </summary>
public class AiInventoryRepositorySummary
{
	/// <summary>Name of the repository</summary>
	public required string RepositoryName { get; set; }

	/// <summary>Number of distinct locations with matching inventory items</summary>
	public required int LocationsCount { get; set; }

	/// <summary>Total number of inventory item references in this repository</summary>
	public required int ReferencesCount { get; set; }
}
