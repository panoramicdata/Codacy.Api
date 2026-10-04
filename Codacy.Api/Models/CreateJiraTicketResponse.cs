namespace Codacy.Api.Models;

/// <summary>
/// Create Jira ticket response
/// </summary>
public class CreateJiraTicketResponse
{
	/// <summary>The created ticket</summary>
	public required JiraTicket Data { get; set; }
}
