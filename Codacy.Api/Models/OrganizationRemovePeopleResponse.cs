namespace Codacy.Api.Models;

/// <summary>
/// Remove people from organization response
/// </summary>
public class OrganizationRemovePeopleResponse
{
	/// <summary>Successfully removed</summary>
	public List<RemovePeopleEmailStatus>? Success { get; set; }

	/// <summary>Failed to remove</summary>
	public List<RemovePeopleEmailStatus>? Failed { get; set; }
}
