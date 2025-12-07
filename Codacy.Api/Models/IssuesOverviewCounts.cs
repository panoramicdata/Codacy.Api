namespace Codacy.Api.Models;

/// <summary>
/// Issues overview counts
/// </summary>
public class IssuesOverviewCounts
{
	/// <summary>Counts by category</summary>
	public required List<Count> Categories { get; set; }

	/// <summary>Counts by language</summary>
	public required List<Count> Languages { get; set; }

	/// <summary>Counts by severity level</summary>
	public required List<Count> Levels { get; set; }

	/// <summary>Counts by tag</summary>
	public required List<Count> Tags { get; set; }

	/// <summary>Counts by pattern</summary>
	public required List<PatternsCount> Patterns { get; set; }

	/// <summary>Counts by author</summary>
	public required List<Count> Authors { get; set; }
}
