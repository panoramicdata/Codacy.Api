namespace Codacy.Api.Models;

/// <summary>
/// Pull request information
/// </summary>
public class PullRequest
{
	/// <summary>Pull request ID</summary>
	public required long Id { get; set; }

	/// <summary>Pull request number</summary>
	public required int Number { get; set; }

	/// <summary>Last updated timestamp</summary>
	public required DateTimeOffset Updated { get; set; }

	/// <summary>Pull request status</summary>
	public required string Status { get; set; }

	/// <summary>Repository name</summary>
	public required string Repository { get; set; }

	/// <summary>Pull request title</summary>
	public required string Title { get; set; }

	/// <summary>Pull request owner</summary>
	public required PullRequestOwner Owner { get; set; }

	/// <summary>Head commit SHA</summary>
	public required string HeadCommitSha { get; set; }

	/// <summary>Common ancestor commit SHA</summary>
	public required string CommonAncestorCommitSha { get; set; }

	/// <summary>Origin branch</summary>
	public string? OriginBranch { get; set; }

	/// <summary>Target branch</summary>
	public string? TargetBranch { get; set; }

	/// <summary>Git provider URL</summary>
	public required string GitHref { get; set; }
}

/// <summary>
/// Pull request owner
/// </summary>
public class PullRequestOwner
{
	/// <summary>Owner name</summary>
	public required string Name { get; set; }

	/// <summary>Avatar URL</summary>
	public string? AvatarUrl { get; set; }

	/// <summary>Username</summary>
	public string? Username { get; set; }

	/// <summary>Email</summary>
	public string? Email { get; set; }
}

/// <summary>
/// Pull request with analysis
/// </summary>
public class PullRequestWithAnalysis
{
	/// <summary>Is up to standards</summary>
	public bool? IsUpToStandards { get; set; }

	/// <summary>Is currently analyzing</summary>
	public required bool IsAnalysing { get; set; }

	/// <summary>Pull request information</summary>
	public required PullRequest PullRequest { get; set; }

	/// <summary>New issues count</summary>
	public int? NewIssues { get; set; }

	/// <summary>Fixed issues count</summary>
	public int? FixedIssues { get; set; }

	/// <summary>Complexity delta</summary>
	public int? DeltaComplexity { get; set; }

	/// <summary>Clones count delta</summary>
	public int? DeltaClonesCount { get; set; }

	/// <summary>Coverage information</summary>
	public PullRequestAnalysisCoverage? Coverage { get; set; }

	/// <summary>Quality analysis</summary>
	public QualityAnalysis? Quality { get; set; }

	/// <summary>Analysis metadata</summary>
	public required AnalysisMeta Meta { get; set; }
}

/// <summary>
/// Pull request analysis coverage information
/// </summary>
public class PullRequestAnalysisCoverage
{
	/// <summary>Delta coverage percentage</summary>
	public double? DeltaCoverage { get; set; }

	/// <summary>Diff coverage</summary>
	public DiffCoverage? DiffCoverage { get; set; }

	/// <summary>Is up to standards</summary>
	public bool? IsUpToStandards { get; set; }

	/// <summary>Result reasons</summary>
	public List<AnalysisResultReason>? ResultReasons { get; set; }
}

/// <summary>
/// Diff coverage
/// </summary>
public class DiffCoverage
{
	/// <summary>Coverage value</summary>
	public double? Value { get; set; }

	/// <summary>Covered lines</summary>
	public int? CoveredLines { get; set; }

	/// <summary>Coverable lines</summary>
	public int? CoverableLines { get; set; }

	/// <summary>Cause/reason</summary>
	public required string Cause { get; set; }
}

/// <summary>
/// Pull request with analysis list response
/// </summary>
public class PullRequestWithAnalysisListResponse
{
	/// <summary>Pagination info</summary>
	public PaginationInfo? Pagination { get; set; }

	/// <summary>Pull requests with analysis</summary>
	public required List<PullRequestWithAnalysis> Data { get; set; }
}
