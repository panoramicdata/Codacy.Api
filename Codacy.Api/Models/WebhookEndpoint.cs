namespace Codacy.Api.Models;

/// <summary>
/// A webhook endpoint configured for an organization
/// </summary>
public class WebhookEndpoint
{
	/// <summary>Webhook endpoint identifier</summary>
	public required Guid Id { get; set; }

	/// <summary>URL that Codacy POSTs deliveries to</summary>
	public required string Url { get; set; }

	/// <summary>When the endpoint was created</summary>
	public required DateTimeOffset CreatedAt { get; set; }
}
