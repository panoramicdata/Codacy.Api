namespace Codacy.Api.Models;

// ===== Coverage Models =====

/// <summary>
/// Pull request with coverage response
/// </summary>
public class PullRequestWithCoverageResponse
{
	/// <summary>Coverage data</summary>
	public required PullRequestCoverage Data { get; set; }
}

/// <summary>
/// Pull request coverage
/// </summary>
public class PullRequestCoverage
{
	/// <summary>Common ancestor commit SHA</summary>
	public string? CommonAncestor { get; set; }

	/// <summary>Head commit SHA</summary>
	public required string Head { get; set; }

	/// <summary>Coverage delta</summary>
	public double? DeltaCoverage { get; set; }

	/// <summary>Total coverage</summary>
	public double? TotalCoverage { get; set; }

	/// <summary>Is up to standards</summary>
	public bool? IsUpToStandards { get; set; }

	/// <summary>Result reasons</summary>
	public List<AnalysisResultReason>? ResultReasons { get; set; }
}

/// <summary>
/// Pull request files coverage response
/// </summary>
public class PullRequestFilesCoverageResponse
{
	/// <summary>Files coverage data</summary>
	public required List<FileCoverage> Data { get; set; }
}

/// <summary>
/// File coverage
/// </summary>
public class FileCoverage
{
	/// <summary>File path</summary>
	public required string FilePath { get; set; }

	/// <summary>Coverage percentage</summary>
	public double? Coverage { get; set; }

	/// <summary>Delta coverage</summary>
	public double? DeltaCoverage { get; set; }

	/// <summary>Covered lines</summary>
	public int? CoveredLines { get; set; }

	/// <summary>Coverable lines</summary>
	public int? CoverableLines { get; set; }
}

/// <summary>
/// Coverage pull request response
/// </summary>
public class CoveragePullRequestResponse
{
	/// <summary>Coverage reports</summary>
	public required List<CoverageReport> Data { get; set; }
}

/// <summary>
/// Coverage report
/// </summary>
public class CoverageReport
{
	/// <summary>Commit SHA</summary>
	public required string CommitSha { get; set; }

	/// <summary>Upload timestamp</summary>
	public required DateTimeOffset UploadTimestamp { get; set; }

	/// <summary>Coverage percentage</summary>
	public required double CoveragePercentage { get; set; }

	/// <summary>Files covered</summary>
	public required int FilesCovered { get; set; }
}
