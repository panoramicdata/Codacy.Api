namespace Codacy.Api.Models;

/// <summary>
/// Response containing the overview of the patterns in a tool
/// </summary>
public class ToolPatternsOverviewResponse
{
	/// <summary>Patterns overview</summary>
	public required ToolPatternsOverview Data { get; set; }
}
