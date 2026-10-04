namespace Codacy.Api.Models;

/// <summary>
/// A Codacy element to include in a Jira ticket
/// </summary>
public class CreateJiraTicketElement
{
	/// <summary>Element identifier</summary>
	public required string ElementId { get; set; }

	/// <summary>Repository name</summary>
	public string? RepositoryName { get; set; }
}
