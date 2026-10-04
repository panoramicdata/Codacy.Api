namespace Codacy.Api.Models;

/// <summary>
/// Request body to add an organization to Codacy
/// </summary>
public class AddOrganizationBody
{
	/// <summary>Git provider</summary>
	public required Provider Provider { get; set; }

	/// <summary>Remote identifier</summary>
	public required string RemoteIdentifier { get; set; }

	/// <summary>Organization name</summary>
	public required string Name { get; set; }

	/// <summary>Organization type</summary>
	public required OrganizationType Type { get; set; }

	/// <summary>Products</summary>
	public List<CodacyProduct>? Products { get; set; }
}
