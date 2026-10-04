namespace Codacy.Api.Models;

/// <summary>
/// Jira tickets retrieval response
/// </summary>
public class GetJiraTicketsResponse
{
	/// <summary>The Jira tickets</summary>
	public required List<JiraTicket> Data { get; set; }
}
