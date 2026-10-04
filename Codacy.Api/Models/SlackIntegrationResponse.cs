namespace Codacy.Api.Models;

/// <summary>
/// A response with a Slack integration
/// </summary>
public class SlackIntegrationResponse
{
	/// <summary>The Slack integration</summary>
	public required SlackIntegration Data { get; set; }
}
