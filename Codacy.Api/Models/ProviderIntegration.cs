namespace Codacy.Api.Models;

/// <summary>
/// Provider integration existing on the platform
/// </summary>
public class ProviderIntegration
{
	/// <summary>Git provider</summary>
	public required Provider Provider { get; set; }

	/// <summary>Redirect URL</summary>
	public required string RedirectUrl { get; set; }
}
