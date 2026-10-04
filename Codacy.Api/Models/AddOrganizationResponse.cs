namespace Codacy.Api.Models;

/// <summary>
/// Response of adding an organization
/// </summary>
public class AddOrganizationResponse
{
	/// <summary>Organization</summary>
	public required Organization Organization { get; set; }

	/// <summary>Warnings</summary>
	public List<string>? Warnings { get; set; }
}
