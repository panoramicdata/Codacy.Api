namespace Codacy.Api.Models;

/// <summary>
/// File coverage analysis
/// </summary>
public class FileCoverageAnalysis
{
	/// <summary>Coverage percentage</summary>
	public required double Coverage { get; set; }

	/// <summary>Coverable lines</summary>
	public required long CoverableLines { get; set; }

	/// <summary>Covered lines</summary>
	public required long CoveredLines { get; set; }
}
