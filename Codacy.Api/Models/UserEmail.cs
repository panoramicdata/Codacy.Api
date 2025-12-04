namespace Codacy.Api.Models;

/// <summary>
/// User email
/// </summary>
public class UserEmail
{
	/// <summary>Email address</summary>
	public required string Email { get; set; }

	/// <summary>Is private</summary>
	public required bool IsPrivate { get; set; }
}
