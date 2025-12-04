namespace Codacy.Api.Models;

/// <summary>
/// API token
/// </summary>
public class ApiToken
{
	/// <summary>Token ID</summary>
	public required long Id { get; set; }

	/// <summary>Token value</summary>
	public required string Token { get; set; }

	/// <summary>Expiration date</summary>
	public DateTimeOffset? ExpiresAt { get; set; }
}
