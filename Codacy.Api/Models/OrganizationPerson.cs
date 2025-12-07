namespace Codacy.Api.Models;

/// <summary>
/// Organization person/committer information
/// </summary>
public class OrganizationPerson
{
	/// <summary>Name</summary>
	public string? Name { get; set; }

	/// <summary>Email</summary>
	public required string Email { get; set; }

	/// <summary>Emails</summary>
	public required List<string> Emails { get; set; }

	/// <summary>User ID</summary>
	public long? UserId { get; set; }

	/// <summary>Committer ID</summary>
	public long? CommitterId { get; set; }

	/// <summary>Last login</summary>
	public DateTimeOffset? LastLogin { get; set; }

	/// <summary>Last analysis</summary>
	public DateTimeOffset? LastAnalysis { get; set; }

	/// <summary>Is active</summary>
	public bool? IsActive { get; set; }

	/// <summary>Can be removed</summary>
	public required bool CanBeRemoved { get; set; }
}
