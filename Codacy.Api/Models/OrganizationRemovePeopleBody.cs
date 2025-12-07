namespace Codacy.Api.Models;

/// <summary>
/// Remove people from organization body
/// </summary>
public class OrganizationRemovePeopleBody
{
	/// <summary>Emails to remove</summary>
	public required List<string> Emails { get; set; }
}
