namespace Codacy.Api.Models;

/// <summary>
/// User emails response
/// </summary>
public class UserEmailsResponse
{
	/// <summary>Email data</summary>
	public required UserEmails Data { get; set; }
}
