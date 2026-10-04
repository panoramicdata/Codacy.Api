namespace Codacy.Api.Models;

/// <summary>
/// The webhook endpoints configured for an organization. Not paginated.
/// </summary>
public class WebhookEndpointList
{
	/// <summary>Webhook endpoints</summary>
	public required List<WebhookEndpoint> Data { get; set; }

	/// <summary>Number of webhook endpoints configured for the organization</summary>
	public required int Count { get; set; }

	/// <summary>Maximum number of webhook endpoints allowed per organization</summary>
	public required int Limit { get; set; }
}
