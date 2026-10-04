namespace Codacy.Api.Models;

/// <summary>
/// A newly created webhook endpoint. This is the only time the signing secret is returned, so
/// store it straight away.
/// </summary>
public class WebhookEndpointCreated : WebhookEndpoint
{
	/// <summary>Secret used to verify the HMAC-SHA256 signature of each delivery</summary>
	public required string Secret { get; set; }
}
