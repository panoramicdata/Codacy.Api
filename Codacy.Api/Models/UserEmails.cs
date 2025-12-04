namespace Codacy.Api.Models;

/// <summary>
/// User emails
/// </summary>
public class UserEmails
{
	/// <summary>Main email</summary>
	public required UserEmail MainEmail { get; set; }

	/// <summary>Other emails</summary>
	public required List<UserEmail> OtherEmails { get; set; }
}
