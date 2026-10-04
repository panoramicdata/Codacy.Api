namespace Codacy.Api.Models;

/// <summary>
/// Request body to create a webhook endpoint
/// </summary>
public class CreateWebhookEndpointBody
{
	/// <summary>URL that Codacy POSTs deliveries to</summary>
	public required string Url { get; set; }
}
