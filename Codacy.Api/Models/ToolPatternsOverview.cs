namespace Codacy.Api.Models;

/// <summary>
/// Overview of the patterns in a tool
/// </summary>
public class ToolPatternsOverview
{
	/// <summary>Pattern counts</summary>
	public required ToolPatternsOverviewCounts Counts { get; set; }
}
