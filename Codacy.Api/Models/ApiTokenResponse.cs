namespace Codacy.Api.Models;

/// <summary>
/// API token response
/// </summary>
public class ApiTokenResponse
{
	/// <summary>The API token</summary>
	public required ApiToken Data { get; set; }
}
