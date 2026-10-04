namespace Codacy.Api.Models;

/// <summary>
/// A repository that has AI inventory items
/// </summary>
public class AiInventoryRepositoryInfo
{
	/// <summary>Name of the repository</summary>
	public required string Name { get; set; }

	/// <summary>Owner of the repository</summary>
	public required string Owner { get; set; }
}
