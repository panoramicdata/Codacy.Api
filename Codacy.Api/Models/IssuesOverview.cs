namespace Codacy.Api.Models;

/// <summary>
/// Issues overview
/// </summary>
public class IssuesOverview
{
	/// <summary>Counts</summary>
	public required IssuesOverviewCounts Counts { get; set; }
}
