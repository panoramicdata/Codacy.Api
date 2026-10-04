namespace Codacy.Api.Models;

/// <summary>
/// Identifies the element to unlink from a Jira ticket
/// </summary>
public class UnlinkRepositoryJiraTicketBody
{
	/// <summary>Type of Codacy element</summary>
	public required ElementType ElementType { get; set; }

	/// <summary>Element identifier</summary>
	public required string ElementId { get; set; }
}
