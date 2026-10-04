namespace Codacy.Api.Models;

/// <summary>
/// Admin entity group
/// </summary>
public class AdminEntityGroup
{
	/// <summary>Name to be used on URLs that identify the group for this type of entity</summary>
	public required string Slug { get; set; }

	/// <summary>Entities in the group</summary>
	public required List<AdminEntity> Items { get; set; }
}
