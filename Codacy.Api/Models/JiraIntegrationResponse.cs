namespace Codacy.Api.Models;

/// <summary>
/// A response with a Jira integration
/// </summary>
public class JiraIntegrationResponse
{
	/// <summary>The Jira integration</summary>
	public required JiraIntegration Data { get; set; }
}
