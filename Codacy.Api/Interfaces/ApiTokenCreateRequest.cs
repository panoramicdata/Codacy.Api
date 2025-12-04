namespace Codacy.Api.Interfaces;

/// <summary>
/// API token create request
/// </summary>
public class ApiTokenCreateRequest
{
	/// <summary>Expiration date</summary>
	public DateTimeOffset? ExpiresAt { get; set; }
}
