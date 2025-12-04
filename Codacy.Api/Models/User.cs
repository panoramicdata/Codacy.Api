namespace Codacy.Api.Models;

/// <summary>
/// User information
/// </summary>
public class User
{
	/// <summary>User ID</summary>
	public required long Id { get; set; }

	/// <summary>Name</summary>
	public string? Name { get; set; }

	/// <summary>Main email</summary>
	public required string MainEmail { get; set; }

	/// <summary>Other emails</summary>
	public required List<string> OtherEmails { get; set; }

	/// <summary>Is admin</summary>
	public required bool IsAdmin { get; set; }

	/// <summary>Is active</summary>
	public required bool IsActive { get; set; }

	/// <summary>Created timestamp</summary>
	public required DateTimeOffset Created { get; set; }

	/// <summary>Intercom hash</summary>
	public string? IntercomHash { get; set; }

	/// <summary>Zendesk hash</summary>
	public string? ZendeskHash { get; set; }

	/// <summary>Should do client qualification</summary>
	public bool? ShouldDoClientQualification { get; set; }
}
