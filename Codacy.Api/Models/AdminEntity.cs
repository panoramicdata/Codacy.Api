namespace Codacy.Api.Models;

/// <summary>
/// Admin entity
/// </summary>
public class AdminEntity
{
	/// <summary>Internal Codacy identifier for this entity</summary>
	public required long Id { get; set; }

	/// <summary>Name to be used on URLs that identify the group for this type of entity</summary>
	public required string GroupSlug { get; set; }

	/// <summary>Human friendly name</summary>
	public required string DisplayName { get; set; }

	/// <summary>Entity details</summary>
	public required Dictionary<string, string> Details { get; set; }

	/// <summary>Related entities</summary>
	public required List<AdminEntityIdentification> RelatedEntities { get; set; }

	/// <summary>Available resources</summary>
	public required List<string> AvailableResources { get; set; }

	/// <summary>Available actions</summary>
	public required List<AdminEntityActionMetadata> AvailableActions { get; set; }
}
