using System.Text.Json.Serialization;

namespace Codacy.Api.Models;

/// <summary>
/// Details of a Slack integration
/// </summary>
public class SlackIntegration
{
	/// <summary>Codacy organization ID</summary>
	[JsonPropertyName("organization_id")]
	public required long OrganizationId { get; set; }

	/// <summary>Slack Incoming Webhook URL to post notifications to</summary>
	[JsonPropertyName("webhook_url")]
	public required string WebhookUrl { get; set; }

	/// <summary>Creation date</summary>
	[JsonPropertyName("created_at")]
	public required DateTimeOffset CreatedAt { get; set; }

	/// <summary>Last update date</summary>
	[JsonPropertyName("updated_at")]
	public required DateTimeOffset UpdatedAt { get; set; }
}
