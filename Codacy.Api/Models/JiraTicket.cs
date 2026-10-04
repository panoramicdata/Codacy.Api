namespace Codacy.Api.Models;

/// <summary>
/// A Jira ticket
/// </summary>
public class JiraTicket
{
	/// <summary>Ticket identifier</summary>
	public required string Id { get; set; }

	/// <summary>Ticket key</summary>
	public required string Key { get; set; }

	/// <summary>Ticket summary</summary>
	public required string Summary { get; set; }

	/// <summary>Ticket assignee</summary>
	public required string Assignee { get; set; }

	/// <summary>Link to the ticket</summary>
	public required string Link { get; set; }

	/// <summary>Ticket status</summary>
	public required JiraTicketStatus Status { get; set; }
}
