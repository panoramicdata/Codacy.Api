namespace Codacy.Api.Models;

/// <summary>
/// Organization in an enterprise
/// </summary>
public class EnterpriseOrganization
{
	/// <summary>Internal Codacy organization id</summary>
	public long? Id { get; set; }

	/// <summary>Organization ID in provider</summary>
	public required string RemoteId { get; set; }

	/// <summary>Organization name</summary>
	public required string Name { get; set; }

	/// <summary>Organization display name</summary>
	public string? DisplayName { get; set; }

	/// <summary>User role in the organization</summary>
	public EnterpriseUserRole? UserRole { get; set; }

	/// <summary>Organization URL</summary>
	public required string Url { get; set; }

	/// <summary>Organization avatar URL</summary>
	public required string Avatar { get; set; }
}
