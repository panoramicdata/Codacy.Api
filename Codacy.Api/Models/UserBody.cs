namespace Codacy.Api.Models;

/// <summary>
/// User body for updates
/// </summary>
public class UserBody
{
	/// <summary>Name</summary>
	public string? Name { get; set; }

	/// <summary>Should do client qualification</summary>
	public bool? ShouldDoClientQualification { get; set; }
}
