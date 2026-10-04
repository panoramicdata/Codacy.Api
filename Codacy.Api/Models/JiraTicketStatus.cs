namespace Codacy.Api.Models;

/// <summary>
/// Status of a Jira ticket
/// </summary>
public class JiraTicketStatus
{
	/// <summary>Status key</summary>
	public required string Key { get; set; }

	/// <summary>Status labels</summary>
	public required List<string> Labels { get; set; }

	/// <summary>Status color</summary>
	public required string Color { get; set; }
}
