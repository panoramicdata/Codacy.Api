using System.Text.Json.Serialization;

namespace Codacy.Api.Models;

/// <summary>
/// The request body to create or update the Slack integration of the organization
/// </summary>
public class SlackIntegrationRequest
{
	/// <summary>Slack Incoming Webhook URL to post notifications to</summary>
	[JsonPropertyName("webhook_url")]
	public required string WebhookUrl { get; set; }
}
