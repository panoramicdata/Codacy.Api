namespace Codacy.Api.Models;

/// <summary>
/// Remove people email status
/// </summary>
public class RemovePeopleEmailStatus
{
	/// <summary>Email</summary>
	public required string Email { get; set; }

	/// <summary>Error message</summary>
	public string? Error { get; set; }
}
