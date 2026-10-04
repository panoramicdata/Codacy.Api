namespace Codacy.Api.Models;

/// <summary>
/// Counts for a given category group
/// </summary>
public class CategoryGroupCount
{
	/// <summary>Inventory category group</summary>
	public required string CategoryGroup { get; set; }

	/// <summary>Number of distinct repositories with items in this category group</summary>
	public int? RepositoriesCount { get; set; }

	/// <summary>Number of distinct (category, marker) pairs in this category group</summary>
	public required int Count { get; set; }
}
