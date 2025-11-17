namespace Codacy.Api.Models;

// ===== Security and Risk Management Models =====

/// <summary>
/// Security/SRM items response
/// </summary>
public class SrmItemsResponse
{
	/// <summary>Security items</summary>
	public required List<SrmItem> Data { get; set; }

	/// <summary>Pagination</summary>
	public PaginationInfo? Pagination { get; set; }
}

/// <summary>
/// Security/SRM item response
/// </summary>
public class SrmItemResponse
{
	/// <summary>Security item data</summary>
	public required SrmItem Data { get; set; }
}

/// <summary>
/// Security/SRM item
/// </summary>
public class SrmItem
{
	/// <summary>Item ID</summary>
	public required Guid Id { get; set; }

	/// <summary>Title</summary>
	public required string Title { get; set; }

	/// <summary>Description</summary>
	public string? Description { get; set; }

	/// <summary>Severity</summary>
	public required string Severity { get; set; }

	/// <summary>Category</summary>
	public required string Category { get; set; }

	/// <summary>Repository</summary>
	public required string Repository { get; set; }

	/// <summary>File path</summary>
	public string? FilePath { get; set; }

	/// <summary>Line number</summary>
	public int? LineNumber { get; set; }

	/// <summary>Status</summary>
	public required string Status { get; set; }

	/// <summary>Is ignored</summary>
	public required bool IsIgnored { get; set; }

	/// <summary>Ignored reason</summary>
	public string? IgnoredReason { get; set; }

	/// <summary>Ignored by</summary>
	public string? IgnoredBy { get; set; }

	/// <summary>Ignored at</summary>
	public DateTimeOffset? IgnoredAt { get; set; }

	/// <summary>First detected</summary>
	public required DateTimeOffset FirstDetected { get; set; }

	/// <summary>Last detected</summary>
	public required DateTimeOffset LastDetected { get; set; }

	/// <summary>CVSS score</summary>
	public double? CvssScore { get; set; }

	/// <summary>CWE ID</summary>
	public string? CweId { get; set; }

	/// <summary>CVE ID</summary>
	public string? CveId { get; set; }

	/// <summary>Package name</summary>
	public string? PackageName { get; set; }

	/// <summary>Package version</summary>
	public string? PackageVersion { get; set; }

	/// <summary>Fixed version</summary>
	public string? FixedVersion { get; set; }
}

/// <summary>
/// Search SRM items request
/// </summary>
public class SearchSRMItems
{
	/// <summary>Search query</summary>
	public string? Query { get; set; }

	/// <summary>Severity filter</summary>
	public List<string>? Severities { get; set; }

	/// <summary>Category filter</summary>
	public List<string>? Categories { get; set; }

	/// <summary>Status filter</summary>
	public List<string>? Status { get; set; }

	/// <summary>Repository filter</summary>
	public List<string>? Repositories { get; set; }

	/// <summary>Include ignored items</summary>
	public bool? IncludeIgnored { get; set; }

	/// <summary>Date range start</summary>
	public DateTimeOffset? DateFrom { get; set; }

	/// <summary>Date range end</summary>
	public DateTimeOffset? DateTo { get; set; }
}

/// <summary>
/// Ignore SRM item body
/// </summary>
public class IgnoreSRMItemBody
{
	/// <summary>Reason for ignoring</summary>
	public required string Reason { get; set; }

	/// <summary>Additional notes</summary>
	public string? Notes { get; set; }
}

/// <summary>
/// SRM dashboard response
/// </summary>
public class SRMDashboardResponse
{
	/// <summary>Dashboard data</summary>
	public required SRMDashboard Data { get; set; }
}

/// <summary>
/// SRM dashboard
/// </summary>
public class SRMDashboard
{
	/// <summary>Total items</summary>
	public int? TotalItems { get; set; }

	/// <summary>Critical items</summary>
	public int? CriticalItems { get; set; }

	/// <summary>High severity items</summary>
	public int? HighItems { get; set; }

	/// <summary>Medium severity items</summary>
	public int? MediumItems { get; set; }

	/// <summary>Low severity items</summary>
	public int? LowItems { get; set; }

	/// <summary>Items by category</summary>
	public Dictionary<string, int>? ItemsByCategory { get; set; }

	/// <summary>Trend data</summary>
	public List<SRMTrendData>? Trend { get; set; }
}

/// <summary>
/// SRM trend data point
/// </summary>
public class SRMTrendData
{
	/// <summary>Date</summary>
	public required DateTimeOffset Date { get; set; }

	/// <summary>Count</summary>
	public required int Count { get; set; }
}

/// <summary>
/// Search SRM dashboard request
/// </summary>
public class SearchSRMDashboard
{
	/// <summary>Repository filter</summary>
	public List<string>? Repositories { get; set; }

	/// <summary>Date range start</summary>
	public DateTimeOffset? DateFrom { get; set; }

	/// <summary>Date range end</summary>
	public DateTimeOffset? DateTo { get; set; }
}

/// <summary>
/// SRM dashboard repositories response
/// </summary>
public class SRMDashboardRepositoriesResponse
{
	/// <summary>Repository data</summary>
	public required List<SRMDashboardRepository> Data { get; set; }
}

/// <summary>
/// SRM dashboard repository
/// </summary>
public class SRMDashboardRepository
{
	/// <summary>Repository name</summary>
	public string? Repository { get; set; }

	/// <summary>Total items</summary>
	public int? TotalItems { get; set; }

	/// <summary>Critical items</summary>
	public int? CriticalItems { get; set; }

	/// <summary>High severity items</summary>
	public int? HighItems { get; set; }
}

/// <summary>
/// Search SRM dashboard repositories request
/// </summary>
public class SearchSRMDashboardRepositories
{
	/// <summary>Repository filter</summary>
	public List<string>? Repositories { get; set; }
}

/// <summary>
/// SRM dashboard history response
/// </summary>
public class SRMDashboardHistoryResponse
{
	/// <summary>History data</summary>
	public required List<SRMDashboardHistoryPoint> Data { get; set; }
}

/// <summary>
/// SRM dashboard history data point
/// </summary>
public class SRMDashboardHistoryPoint
{
	/// <summary>Date</summary>
	public DateTimeOffset? Date { get; set; }

	/// <summary>Total items</summary>
	public int? TotalItems { get; set; }

	/// <summary>Critical items</summary>
	public int? CriticalItems { get; set; }

	/// <summary>High severity items</summary>
	public int? HighItems { get; set; }
}

/// <summary>
/// Search SRM dashboard history request
/// </summary>
public class SearchSRMDashboardHistory
{
	/// <summary>Date range start</summary>
	public DateTimeOffset? DateFrom { get; set; }

	/// <summary>Date range end</summary>
	public DateTimeOffset? DateTo { get; set; }

	/// <summary>Repository filter</summary>
	public List<string>? Repositories { get; set; }
}

/// <summary>
/// SRM dashboard categories response
/// </summary>
public class SRMDashboardCategoriesResponse
{
	/// <summary>Category data</summary>
	public required List<SRMDashboardCategory> Data { get; set; }
}

/// <summary>
/// SRM dashboard category
/// </summary>
public class SRMDashboardCategory
{
	/// <summary>Category name</summary>
	public string? Category { get; set; }

	/// <summary>Item count</summary>
	public int? Count { get; set; }
}

/// <summary>
/// Search SRM dashboard categories request
/// </summary>
public class SearchSRMDashboardCategories
{
	/// <summary>Repository filter</summary>
	public List<string>? Repositories { get; set; }
}

/// <summary>
/// Security managers response
/// </summary>
public class SecurityManagersResponse
{
	/// <summary>Security managers</summary>
	public required List<SecurityManager> Data { get; set; }

	/// <summary>Pagination</summary>
	public PaginationInfo? Pagination { get; set; }
}

/// <summary>
/// Security manager
/// </summary>
public class SecurityManager
{
	/// <summary>User ID</summary>
	public required long UserId { get; set; }

	/// <summary>Username</summary>
	public required string Username { get; set; }

	/// <summary>Email</summary>
	public required string Email { get; set; }

	/// <summary>Added at</summary>
	public required DateTimeOffset AddedAt { get; set; }
}

/// <summary>
/// Security manager body
/// </summary>
public class SecurityManagerBody
{
	/// <summary>User ID to add as security manager</summary>
	public required long UserId { get; set; }
}

/// <summary>
/// Security repositories response
/// </summary>
public class SecurityRepositoriesResponse
{
	/// <summary>Security repositories</summary>
	public required List<SecurityRepository> Data { get; set; }

	/// <summary>Pagination</summary>
	public PaginationInfo? Pagination { get; set; }
}

/// <summary>
/// Security repository
/// </summary>
public class SecurityRepository
{
	/// <summary>Repository name</summary>
	public string? Repository { get; set; }

	/// <summary>Total security items</summary>
	public int? TotalItems { get; set; }

	/// <summary>Critical items</summary>
	public int? CriticalItems { get; set; }

	/// <summary>High severity items</summary>
	public int? HighItems { get; set; }
}

/// <summary>
/// Security categories response
/// </summary>
public class SecurityCategoriesResponse
{
	/// <summary>Security categories</summary>
	public required List<SecurityCategory> Data { get; set; }

	/// <summary>Pagination</summary>
	public PaginationInfo? Pagination { get; set; }
}

/// <summary>
/// Security category
/// </summary>
public class SecurityCategory
{
	/// <summary>Category name</summary>
	public string? Category { get; set; }

	/// <summary>Item count</summary>
	public int? Count { get; set; }

	/// <summary>Description</summary>
	public string? Description { get; set; }
}

/// <summary>
/// SRM DAST report upload response
/// </summary>
public class SRMDASTReportUploadResponse
{
	/// <summary>Upload result</summary>
	public required DASTUploadResult Data { get; set; }
}

/// <summary>
/// DAST upload result
/// </summary>
public class DASTUploadResult
{
	/// <summary>Report ID</summary>
	public required Guid ReportId { get; set; }

	/// <summary>Upload timestamp</summary>
	public required DateTimeOffset UploadedAt { get; set; }

	/// <summary>Items found</summary>
	public required int ItemsFound { get; set; }
}

/// <summary>
/// SRM DAST report response
/// </summary>
public class SRMDastReportResponse
{
	/// <summary>DAST reports</summary>
	public required List<DASTReport> Data { get; set; }

	/// <summary>Pagination</summary>
	public PaginationInfo? Pagination { get; set; }
}

/// <summary>
/// DAST report
/// </summary>
public class DASTReport
{
	/// <summary>Report ID</summary>
	public required Guid ReportId { get; set; }

	/// <summary>Tool name</summary>
	public required string ToolName { get; set; }

	/// <summary>Upload timestamp</summary>
	public required DateTimeOffset UploadedAt { get; set; }

	/// <summary>Items found</summary>
	public required int ItemsFound { get; set; }

	/// <summary>Scan target</summary>
	public string? ScanTarget { get; set; }
}

/// <summary>
/// SLA configuration response
/// </summary>
public class SLAConfigResponse
{
	/// <summary>SLA configuration</summary>
	public SLAConfig? Data { get; set; }
}

/// <summary>
/// SLA configuration
/// </summary>
public class SLAConfig
{
	/// <summary>Is enabled</summary>
	public required bool IsEnabled { get; set; }

	/// <summary>Critical threshold (days)</summary>
	public int? CriticalThresholdDays { get; set; }

	/// <summary>High threshold (days)</summary>
	public int? HighThresholdDays { get; set; }

	/// <summary>Medium threshold (days)</summary>
	public int? MediumThresholdDays { get; set; }

	/// <summary>Low threshold (days)</summary>
	public int? LowThresholdDays { get; set; }
}

/// <summary>
/// SLA configuration body
/// </summary>
public class SLAConfigBody
{
	/// <summary>Is enabled</summary>
	public bool? IsEnabled { get; set; }

	/// <summary>Critical threshold (days)</summary>
	public int? CriticalThresholdDays { get; set; }

	/// <summary>High threshold (days)</summary>
	public int? HighThresholdDays { get; set; }

	/// <summary>Medium threshold (days)</summary>
	public int? MediumThresholdDays { get; set; }

	/// <summary>Low threshold (days)</summary>
	public int? LowThresholdDays { get; set; }
}

/// <summary>
/// OSSF Scorecard response
/// </summary>
public class OssfScorecardResponse
{
	/// <summary>Scorecard data</summary>
	public required OssfScorecard Data { get; set; }
}

/// <summary>
/// OSSF Scorecard
/// </summary>
public class OssfScorecard
{
	/// <summary>Repository URL</summary>
	public required string RepositoryUrl { get; set; }

	/// <summary>Overall score</summary>
	public required double Score { get; set; }

	/// <summary>Scorecard version</summary>
	public required string ScorecardVersion { get; set; }

	/// <summary>Check scores</summary>
	public required List<OssfScorecardCheck> Checks { get; set; }
}

/// <summary>
/// OSSF Scorecard check
/// </summary>
public class OssfScorecardCheck
{
	/// <summary>Check name</summary>
	public required string Name { get; set; }

	/// <summary>Score</summary>
	public required double Score { get; set; }

	/// <summary>Reason</summary>
	public string? Reason { get; set; }

	/// <summary>Documentation URL</summary>
	public string? Documentation { get; set; }
}

/// <summary>
/// OSSF Scorecard URL request
/// </summary>
public class OssfScorecardUrlRequest
{
	/// <summary>Repository URL</summary>
	public required string RepositoryUrl { get; set; }
}
