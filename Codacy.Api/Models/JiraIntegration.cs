using System.Text.Json.Serialization;

namespace Codacy.Api.Models;

/// <summary>
/// Details of a Jira integration for the security and risk management feature
/// </summary>
public class JiraIntegration
{
	/// <summary>Codacy organization ID</summary>
	[JsonPropertyName("organization_id")]
	public required long OrganizationId { get; set; }

	/// <summary>Jira cloud ID of the organization</summary>
	[JsonPropertyName("instance_id")]
	public required string InstanceId { get; set; }

	/// <summary>Name of the Jira instance that Codacy has access to</summary>
	[JsonPropertyName("instance_name")]
	public required string InstanceName { get; set; }

	/// <summary>Creation date</summary>
	[JsonPropertyName("created_at")]
	public required DateTimeOffset CreatedAt { get; set; }

	/// <summary>Last update date</summary>
	[JsonPropertyName("updated_at")]
	public required DateTimeOffset UpdatedAt { get; set; }
}
