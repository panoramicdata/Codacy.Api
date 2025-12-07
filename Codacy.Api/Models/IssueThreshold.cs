namespace Codacy.Api.Models;

/// <summary>
/// Issue threshold
/// </summary>
public class IssueThreshold
{
	/// <summary>Threshold value</summary>
	public required int Threshold { get; set; }

	/// <summary>Minimum severity</summary>
	public SeverityLevel? MinimumSeverity { get; set; }
}
