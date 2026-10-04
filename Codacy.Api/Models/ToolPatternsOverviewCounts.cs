namespace Codacy.Api.Models;

/// <summary>
/// Counts of the patterns in a tool
/// </summary>
public class ToolPatternsOverviewCounts
{
	/// <summary>Pattern counts by language</summary>
	public required List<Count> Languages { get; set; }

	/// <summary>Pattern counts by category</summary>
	public required List<Count> Categories { get; set; }

	/// <summary>Pattern counts by severity</summary>
	public required List<Count> Severities { get; set; }

	/// <summary>Pattern counts by tag</summary>
	public required List<Count> Tags { get; set; }

	/// <summary>Total number of recommended patterns</summary>
	public required int TotalRecommended { get; set; }

	/// <summary>Total number of enabled patterns</summary>
	public required int TotalEnabled { get; set; }
}
