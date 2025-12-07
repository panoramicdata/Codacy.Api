namespace Codacy.Api.Models;

/// <summary>
/// Quality gate settings
/// </summary>
public class QualityGate
{
	/// <summary>Issue threshold</summary>
	public IssueThreshold? IssueThreshold { get; set; }

	/// <summary>Security issue threshold</summary>
	public int? SecurityIssueThreshold { get; set; }

	/// <summary>Security issue minimum severity</summary>
	public SeverityLevel? SecurityIssueMinimumSeverity { get; set; }

	/// <summary>Duplication threshold</summary>
	public int? DuplicationThreshold { get; set; }

	/// <summary>Coverage threshold with decimals</summary>
	public double? CoverageThresholdWithDecimals { get; set; }

	/// <summary>Diff coverage threshold</summary>
	public int? DiffCoverageThreshold { get; set; }

	/// <summary>Complexity threshold</summary>
	public int? ComplexityThreshold { get; set; }
}
