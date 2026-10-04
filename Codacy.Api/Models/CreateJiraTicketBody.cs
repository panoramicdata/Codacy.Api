namespace Codacy.Api.Models;

/// <summary>
/// Fields used to create a Jira ticket
/// </summary>
public class CreateJiraTicketBody
{
	/// <summary>Type of Codacy element</summary>
	public required ElementType ElementType { get; set; }

	/// <summary>Jira project identifier</summary>
	public required long JiraProjectId { get; set; }

	/// <summary>Codacy elements to include in the ticket</summary>
	public required List<CreateJiraTicketElement> CreateJiraTicketElements { get; set; }

	/// <summary>Jira issue type identifier</summary>
	public required long IssueTypeId { get; set; }

	/// <summary>Ticket summary</summary>
	public required string Summary { get; set; }

	/// <summary>Description written in Atlassian Document Format</summary>
	public required string Description { get; set; }

	/// <summary>Optional due date</summary>
	public DateOnly? DueDate { get; set; }
}
