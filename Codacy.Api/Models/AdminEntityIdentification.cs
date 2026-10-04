namespace Codacy.Api.Models;

/// <summary>
/// Admin entity identification
/// </summary>
public class AdminEntityIdentification
{
	/// <summary>Internal Codacy identifier for this entity</summary>
	public required long Id { get; set; }

	/// <summary>Name to be used on URLs that identify the group for this type of entity</summary>
	public required string GroupSlug { get; set; }
}
