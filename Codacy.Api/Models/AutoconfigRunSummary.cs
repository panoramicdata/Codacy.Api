namespace Codacy.Api.Models;

/// <summary>
/// Summary of a completed autoconfig run
/// </summary>
public class AutoconfigRunSummary
{
	/// <summary>The autoconfig analysis ID</summary>
	public required Guid AnalysisId { get; set; }

	/// <summary>Repository identifier</summary>
	public required long RepositoryId { get; set; }

	/// <summary>Total duration of the run in milliseconds</summary>
	public required long DurationMs { get; set; }

	/// <summary>Number of issues found before autoconfig was applied</summary>
	public int? IssuesBefore { get; set; }

	/// <summary>Number of issues found after autoconfig was applied</summary>
	public int? IssuesAfter { get; set; }

	/// <summary>Number of languages detected in the repository</summary>
	public int? LanguageCount { get; set; }

	/// <summary>Number of files analysed</summary>
	public int? FileCount { get; set; }

	/// <summary>Number of patterns enabled before and after this run</summary>
	public AutoconfigBeforeAfter? EnabledPatterns { get; set; }

	/// <summary>Number of tools enabled before and after this run</summary>
	public AutoconfigBeforeAfter? EnabledTools { get; set; }

	/// <summary>Issue counts grouped by category, before and after this run</summary>
	public Dictionary<string, AutoconfigBeforeAfter>? IssuesByCategory { get; set; }

	/// <summary>Issue counts grouped by severity, before and after this run</summary>
	public Dictionary<string, AutoconfigBeforeAfter>? IssuesBySeverity { get; set; }

	/// <summary>Paths recommended to be added to the ignore list</summary>
	public List<AutoconfigRecommendedPath>? RecommendedPathsToIgnore { get; set; }

	/// <summary>Human-readable highlights of the most impactful changes made by this run</summary>
	public List<string>? KeyImprovements { get; set; }

	/// <summary>When the run started</summary>
	public required DateTimeOffset StartedAt { get; set; }

	/// <summary>When the run completed</summary>
	public required DateTimeOffset CompletedAt { get; set; }

	/// <summary>Tools that were enabled or disabled by this run</summary>
	public List<AutoconfigToolChange>? ToolChanges { get; set; }

	/// <summary>Patterns that were enabled, disabled, or updated by this run</summary>
	public List<AutoconfigPatternChange>? PatternChanges { get; set; }

	/// <summary>Changes skipped due to conflicts with existing coding standards</summary>
	public List<AutoconfigConflict>? Conflicts { get; set; }
}
