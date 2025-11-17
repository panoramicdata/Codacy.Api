namespace Codacy.Api.Models;

/// <summary>
/// Commit information
/// </summary>
public class Commit
{
	/// <summary>Commit SHA</summary>
	public required string Sha { get; set; }

	/// <summary>Internal commit ID</summary>
	public required long Id { get; set; }

	/// <summary>Commit timestamp</summary>
	public required DateTimeOffset CommitTimestamp { get; set; }

	/// <summary>Author name</summary>
	public required string AuthorName { get; set; }

	/// <summary>Author email</summary>
	public required string AuthorEmail { get; set; }

	/// <summary>Commit message</summary>
	public required string Message { get; set; }

	/// <summary>Analysis start time</summary>
	public DateTimeOffset? StartedAnalysis { get; set; }

	/// <summary>Analysis end time</summary>
	public DateTimeOffset? EndedAnalysis { get; set; }

	/// <summary>Is merge commit</summary>
	public bool? IsMergeCommit { get; set; }

	/// <summary>Git provider URL</summary>
	public string? GitHref { get; set; }

	/// <summary>Parent commit SHAs</summary>
	public List<string>? Parents { get; set; }
}

/// <summary>
/// Commit with analysis data
/// </summary>
public class CommitWithAnalysis
{
	/// <summary>Commit information</summary>
	public required Commit Commit { get; set; }

	/// <summary>Coverage analysis</summary>
	public CoverageAnalysis? Coverage { get; set; }

	/// <summary>Quality analysis</summary>
	public QualityAnalysis? Quality { get; set; }

	/// <summary>Analysis metadata</summary>
	public required AnalysisMeta Meta { get; set; }
}

/// <summary>
/// Coverage analysis data
/// </summary>
public class CoverageAnalysis
{
	/// <summary>Total coverage percentage</summary>
	public double? TotalCoveragePercentage { get; set; }

	/// <summary>Coverage delta percentage</summary>
	public double? DeltaCoveragePercentage { get; set; }

	/// <summary>Is up to standards</summary>
	public bool? IsUpToStandards { get; set; }

	/// <summary>Result reasons</summary>
	public List<AnalysisResultReason>? ResultReasons { get; set; }
}

/// <summary>
/// Quality analysis data
/// </summary>
public class QualityAnalysis
{
	/// <summary>New issues count</summary>
	public int? NewIssues { get; set; }

	/// <summary>Fixed issues count</summary>
	public int? FixedIssues { get; set; }

	/// <summary>Complexity delta</summary>
	public int? DeltaComplexity { get; set; }

	/// <summary>Clones count delta</summary>
	public int? DeltaClonesCount { get; set; }

	/// <summary>Is up to standards</summary>
	public bool? IsUpToStandards { get; set; }

	/// <summary>Result reasons</summary>
	public List<AnalysisResultReason>? ResultReasons { get; set; }
}

/// <summary>
/// Analysis result reason
/// </summary>
public class AnalysisResultReason
{
	/// <summary>Gate name</summary>
	public required string Gate { get; set; }

	/// <summary>Expected threshold</summary>
	public required AnalysisExpectedThreshold ExpectedThreshold { get; set; }

	/// <summary>Is up to standards</summary>
	public required bool IsUpToStandards { get; set; }
}

/// <summary>
/// Expected analysis threshold
/// </summary>
public class AnalysisExpectedThreshold
{
	/// <summary>Threshold value</summary>
	public required double Threshold { get; set; }

	/// <summary>Minimum severity</summary>
	public SeverityLevel? MinimumSeverity { get; set; }
}

/// <summary>
/// Analysis metadata
/// </summary>
public class AnalysisMeta
{
	/// <summary>Is analyzable</summary>
	public required bool Analyzable { get; set; }

	/// <summary>Reason</summary>
	public string? Reason { get; set; }
}

/// <summary>
/// Commit with analysis list response
/// </summary>
public class CommitWithAnalysisListResponse
{
	/// <summary>Pagination info</summary>
	public PaginationInfo? Pagination { get; set; }

	/// <summary>Commits with analysis</summary>
	public required List<CommitWithAnalysis> Data { get; set; }
}

/// <summary>
/// Search organization repositories request
/// </summary>
public class SearchOrganizationRepositoriesRequest
{
	/// <summary>Search query</summary>
	public string? Query { get; set; }

	/// <summary>Repository names filter</summary>
	public List<string>? Repositories { get; set; }

	/// <summary>Segments filter</summary>
	public List<string>? Segments { get; set; }

	/// <summary>Languages filter</summary>
	public List<string>? Languages { get; set; }

	/// <summary>Visibility filter</summary>
	public List<Visibility>? Visibility { get; set; }
}

/// <summary>
/// Repository with analysis response
/// </summary>
public class RepositoryWithAnalysisResponse
{
	/// <summary>Repository with analysis data</summary>
	public required RepositoryWithAnalysis Data { get; set; }
}

/// <summary>
/// Repository with analysis
/// </summary>
public class RepositoryWithAnalysis
{
	/// <summary>Last analyzed commit</summary>
	public Commit? LastAnalysedCommit { get; set; }

	/// <summary>Grade (0-100)</summary>
	public int? Grade { get; set; }

	/// <summary>Grade letter (A-F)</summary>
	public string? GradeLetter { get; set; }

	/// <summary>Issues percentage</summary>
	public long? IssuesPercentage { get; set; }

	/// <summary>Issues count</summary>
	public long? IssuesCount { get; set; }

	/// <summary>Lines of code</summary>
	public long? Loc { get; set; }

	/// <summary>Complex files percentage</summary>
	public long? ComplexFilesPercentage { get; set; }

	/// <summary>Complex files count</summary>
	public long? ComplexFilesCount { get; set; }

	/// <summary>Duplication percentage</summary>
	public long? DuplicationPercentage { get; set; }

	/// <summary>Repository information</summary>
	public required Repository Repository { get; set; }

	/// <summary>Selected branch</summary>
	public Branch? SelectedBranch { get; set; }

	/// <summary>Coverage information</summary>
	public Coverage? Coverage { get; set; }
}

/// <summary>
/// Coverage information
/// </summary>
public class Coverage
{
	/// <summary>Files uncovered</summary>
	public long? FilesUncovered { get; set; }

	/// <summary>Files with low coverage</summary>
	public long? FilesWithLowCoverage { get; set; }

	/// <summary>Coverage percentage (truncated)</summary>
	public long? CoveragePercentage { get; set; }

	/// <summary>Coverage percentage with decimals</summary>
	public double? CoveragePercentageWithDecimals { get; set; }

	/// <summary>Total files</summary>
	public int? NumberTotalFiles { get; set; }

	/// <summary>Covered lines</summary>
	public int? NumberCoveredLines { get; set; }

	/// <summary>Coverable lines</summary>
	public int? NumberCoverableLines { get; set; }
}

/// <summary>
/// Analysis tools response
/// </summary>
public class AnalysisToolsResponse
{
	/// <summary>Tools</summary>
	public required List<AnalysisTool> Data { get; set; }
}

/// <summary>
/// Analysis tool
/// </summary>
public class AnalysisTool
{
	/// <summary>Tool UUID</summary>
	public required string Uuid { get; set; }

	/// <summary>Tool name</summary>
	public required string Name { get; set; }

	/// <summary>Is client side</summary>
	public required bool IsClientSide { get; set; }

	/// <summary>Tool settings</summary>
	public required AnalysisToolSettings Settings { get; set; }
}

/// <summary>
/// Analysis tool settings
/// </summary>
public class AnalysisToolSettings
{
	/// <summary>Is enabled</summary>
	public required bool IsEnabled { get; set; }

	/// <summary>Follows standard</summary>
	public required bool FollowsStandard { get; set; }

	/// <summary>Is custom</summary>
	public required bool IsCustom { get; set; }

	/// <summary>Has configuration file</summary>
	public required bool HasConfigurationFile { get; set; }

	/// <summary>Uses configuration file</summary>
	public required bool UsesConfigurationFile { get; set; }

	/// <summary>Enabled by standards</summary>
	public required List<CodingStandardInfo> EnabledBy { get; set; }
}

/// <summary>
/// Coding standard info
/// </summary>
public class CodingStandardInfo
{
	/// <summary>Coding standard ID</summary>
	public required long Id { get; set; }

	/// <summary>Coding standard name</summary>
	public required string Name { get; set; }
}

/// <summary>
/// Repository conflicts response
/// </summary>
public class RepositoryConflictsResponse
{
	/// <summary>Tools with conflicts</summary>
	public required List<ToolConflict> Data { get; set; }
}

/// <summary>
/// Tool conflict
/// </summary>
public class ToolConflict
{
	/// <summary>Tool UUID</summary>
	public required string ToolUuid { get; set; }

	/// <summary>Tool name</summary>
	public required string ToolName { get; set; }

	/// <summary>Number of conflicts</summary>
	public required int ConflictsCount { get; set; }
}

/// <summary>
/// Configure tool body
/// </summary>
public class ConfigureToolBody
{
	/// <summary>Patterns to enable</summary>
	public List<string>? EnablePatterns { get; set; }

	/// <summary>Patterns to disable</summary>
	public List<string>? DisablePatterns { get; set; }
}

/// <summary>
/// First analysis overview response
/// </summary>
public class FirstAnalysisOverviewResponse
{
	/// <summary>First analysis overview data</summary>
	public required FirstAnalysisOverview Data { get; set; }
}

/// <summary>
/// First analysis overview
/// </summary>
public class FirstAnalysisOverview
{
	/// <summary>Is first analysis</summary>
	public required bool IsFirstAnalysis { get; set; }

	/// <summary>Is analyzing</summary>
	public required bool IsAnalyzing { get; set; }

	/// <summary>Started at</summary>
	public DateTimeOffset? StartedAt { get; set; }

	/// <summary>Progress percentage</summary>
	public int? Progress { get; set; }
}

/// <summary>
/// Clones response
/// </summary>
public class ClonesResponse
{
	/// <summary>Clones data</summary>
	public required List<Clone> Data { get; set; }

	/// <summary>Pagination info</summary>
	public PaginationInfo? Pagination { get; set; }
}

/// <summary>
/// Clone (duplicate code block)
/// </summary>
public class Clone
{
	/// <summary>Clone ID</summary>
	public required string CloneId { get; set; }

	/// <summary>Clone hash</summary>
	public required string CloneHash { get; set; }

	/// <summary>Lines of code</summary>
	public required int LinesOfCode { get; set; }

	/// <summary>Number of tokens</summary>
	public required int NumberOfTokens { get; set; }

	/// <summary>Clone fragments</summary>
	public required List<CloneFragment> CloneFragments { get; set; }
}

/// <summary>
/// Clone fragment
/// </summary>
public class CloneFragment
{
	/// <summary>File path</summary>
	public required string FilePath { get; set; }

	/// <summary>Start line</summary>
	public required int StartLine { get; set; }

	/// <summary>End line</summary>
	public required int EndLine { get; set; }
}

/// <summary>
/// Logs response
/// </summary>
public class LogsResponse
{
	/// <summary>Log entries</summary>
	public required List<LogEntry> Data { get; set; }
}

/// <summary>
/// Log entry
/// </summary>
public class LogEntry
{
	/// <summary>Timestamp</summary>
	public required DateTimeOffset Timestamp { get; set; }

	/// <summary>Level</summary>
	public required string Level { get; set; }

	/// <summary>Message</summary>
	public required string Message { get; set; }

	/// <summary>Tool name</summary>
	public string? ToolName { get; set; }
}

/// <summary>
/// Commit analysis statistics
/// </summary>
public class CommitAnalysisStats
{
	/// <summary>Repository ID</summary>
	public required long RepositoryId { get; set; }

	/// <summary>Commit ID</summary>
	public required long CommitId { get; set; }

	/// <summary>Number of issues</summary>
	public required long NumberIssues { get; set; }

	/// <summary>Lines of code</summary>
	public required long NumberLoc { get; set; }

	/// <summary>Issues percentage</summary>
	public required long IssuePercentage { get; set; }

	/// <summary>Coverage percentage</summary>
	public long? CoveragePercentage { get; set; }

	/// <summary>Coverage with decimals</summary>
	public double? CoveragePercentageWithDecimals { get; set; }

	/// <summary>Commit timestamp</summary>
	public required DateTimeOffset CommitTimestamp { get; set; }

	/// <summary>Commit author name</summary>
	public required string CommitAuthorName { get; set; }

	/// <summary>Commit short UUID</summary>
	public required string CommitShortUUID { get; set; }
}

/// <summary>
/// Commit analysis stats list response
/// </summary>
public class CommitAnalysisStatsListResponse
{
	/// <summary>Statistics</summary>
	public required List<CommitAnalysisStats> Data { get; set; }
}

/// <summary>
/// Category overview list response
/// </summary>
public class CategoryOverviewListResponse
{
	/// <summary>Category overviews</summary>
	public required List<CategoryOverview> Data { get; set; }
}

/// <summary>
/// Category overview
/// </summary>
public class CategoryOverview
{
	/// <summary>Category</summary>
	public required string Category { get; set; }

	/// <summary>Issue count</summary>
	public required int IssueCount { get; set; }

	/// <summary>File count</summary>
	public required int FileCount { get; set; }
}

/// <summary>
/// Commit delta statistics
/// </summary>
public class CommitDeltaStatistics
{
	/// <summary>Commit UUID</summary>
	public required string CommitUuid { get; set; }

	/// <summary>New issues</summary>
	public required int NewIssues { get; set; }

	/// <summary>Fixed issues</summary>
	public required int FixedIssues { get; set; }

	/// <summary>Delta complexity</summary>
	public int? DeltaComplexity { get; set; }

	/// <summary>Delta coverage</summary>
	public int? DeltaCoverage { get; set; }

	/// <summary>Delta coverage with decimals</summary>
	public double? DeltaCoverageWithDecimals { get; set; }

	/// <summary>Delta clones count</summary>
	public int? DeltaClonesCount { get; set; }

	/// <summary>Is analyzed</summary>
	public required bool Analyzed { get; set; }
}

/// <summary>
/// Commit delta issues response
/// </summary>
public class CommitDeltaIssuesResponse
{
	/// <summary>Delta issues</summary>
	public required List<DeltaIssue> Data { get; set; }

	/// <summary>Pagination info</summary>
	public PaginationInfo? Pagination { get; set; }
}

/// <summary>
/// Delta issue
/// </summary>
public class DeltaIssue
{
	/// <summary>Issue ID</summary>
	public required long IssueId { get; set; }

	/// <summary>File path</summary>
	public required string FilePath { get; set; }

	/// <summary>Line number</summary>
	public required int LineNumber { get; set; }

	/// <summary>Message</summary>
	public required string Message { get; set; }

	/// <summary>Pattern ID</summary>
	public required string PatternId { get; set; }

	/// <summary>Category</summary>
	public required string Category { get; set; }

	/// <summary>Level</summary>
	public required SeverityLevel Level { get; set; }
}

/// <summary>
/// File analysis list response
/// </summary>
public class FileAnalysisListResponse
{
	/// <summary>File analyses</summary>
	public required List<FileAnalysis> Data { get; set; }

	/// <summary>Pagination info</summary>
	public PaginationInfo? Pagination { get; set; }
}

/// <summary>
/// File analysis
/// </summary>
public class FileAnalysis
{
	/// <summary>File path</summary>
	public required string FilePath { get; set; }

	/// <summary>Coverage percentage</summary>
	public double? CoveragePercentage { get; set; }

	/// <summary>Delta coverage percentage</summary>
	public double? DeltaCoveragePercentage { get; set; }

	/// <summary>New issues count</summary>
	public int? NewIssuesCount { get; set; }

	/// <summary>Fixed issues count</summary>
	public int? FixedIssuesCount { get; set; }

	/// <summary>Total issues count</summary>
	public int? TotalIssuesCount { get; set; }
}
