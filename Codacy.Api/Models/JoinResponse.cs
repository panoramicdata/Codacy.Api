namespace Codacy.Api.Models;

/// <summary>
/// Join response
/// </summary>
public class JoinResponse
{
	/// <summary>Organization identifier</summary>
	public required long OrganizationIdentifier { get; set; }

	/// <summary>Join status</summary>
	public required JoinStatus JoinStatus { get; set; }
}
