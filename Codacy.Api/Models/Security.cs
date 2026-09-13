using System.Text.Json.Serialization;

namespace Codacy.Api.Models;

// ===== Security and Risk Management Models =====
//
// These models mirror the "security" tag of the Codacy OpenAPI specification
// (https://api.codacy.com/api/api-docs/swagger.yaml). Property names, nullability and enum
// members are taken from that specification rather than inferred: an earlier hand-written
// version of this file described fields the API never returns, which made every security
// response fail to deserialize.

/// <summary>
/// The system that raised a security and risk management item.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<SrmSource>))]
public enum SrmSource
{
	/// <summary>Raised by Codacy's own analysis.</summary>
	Codacy,

	/// <summary>Imported from Jira.</summary>
	Jira,

	/// <summary>Raised by a penetration test.</summary>
	PenTest,

	/// <summary>Raised by an OWASP ZAP scan.</summary>
	[JsonStringEnumMemberName("ZAP")]
	Zap,

	/// <summary>Raised by a Trivy scan.</summary>
	Trivy
}

/// <summary>
/// Severity of a security and risk management item.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<SrmPriority>))]
public enum SrmPriority
{
	/// <summary>Low severity.</summary>
	Low,

	/// <summary>Medium severity.</summary>
	Medium,

	/// <summary>High severity.</summary>
	High,

	/// <summary>Critical severity.</summary>
	Critical
}

/// <summary>
/// Lifecycle status of a security and risk management item, relative to its SLA due date.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<SrmStatus>))]
public enum SrmStatus
{
	/// <summary>Open and past its due date.</summary>
	Overdue,

	/// <summary>Open and within its due date.</summary>
	OnTrack,

	/// <summary>Open and approaching its due date.</summary>
	DueSoon,

	/// <summary>Closed within its due date.</summary>
	ClosedOnTime,

	/// <summary>Closed after its due date.</summary>
	ClosedLate,

	/// <summary>Ignored, and so excluded from the SLA.</summary>
	Ignored
}

/// <summary>
/// Records who ignored a security and risk management item, and why.
/// </summary>
public class SrmIgnoredBody
{
	/// <summary>When the item was ignored.</summary>
	public required DateTimeOffset At { get; set; }

	/// <summary>Codacy user ID of the person who ignored the item.</summary>
	public required long AuthorId { get; set; }

	/// <summary>Name of the person who ignored the item.</summary>
	public required string AuthorName { get; set; }

	/// <summary>Reason the item was ignored.</summary>
	public required string Reason { get; set; }
}

/// <summary>
/// Enrichment extracted from the advisory behind an SCA finding.
/// </summary>
public class AdvisoryInformation
{
	/// <summary>The advisory identifier, for example <c>CVE-2024-24786</c>.</summary>
	public required string AdvisoryId { get; set; }

	/// <summary>The vulnerable functions named by the advisory.</summary>
	public required List<string> VulnerableFunctions { get; set; }

	/// <summary>When the advisory was published, as reported by OSV.</summary>
	public DateTimeOffset? PublishedAt { get; set; }
}

/// <summary>
/// Security and risk management item of an organization.
/// </summary>
public class SrmItem
{
	/// <summary>Item ID internal to Codacy.</summary>
	public required Guid Id { get; set; }

	/// <summary>The system that raised the item.</summary>
	public required SrmSource ItemSource { get; set; }

	/// <summary>Original source item ID.</summary>
	public required string ItemSourceId { get; set; }

	/// <summary>Human-readable title of the item.</summary>
	public required string Title { get; set; }

	/// <summary>Repository the item was found in.</summary>
	public string? Repository { get; set; }

	/// <summary>When the item was opened.</summary>
	public required DateTimeOffset OpenedAt { get; set; }

	/// <summary>When the item was closed, if it has been.</summary>
	public DateTimeOffset? ClosedAt { get; set; }

	/// <summary>When the item falls due under the organization's SLA.</summary>
	public required DateTimeOffset DueAt { get; set; }

	/// <summary>Who ignored the item, and why; <see langword="null"/> unless it is ignored.</summary>
	public SrmIgnoredBody? Ignored { get; set; }

	/// <summary>Severity of the item.</summary>
	public required SrmPriority Priority { get; set; }

	/// <summary>Lifecycle status of the item.</summary>
	public required SrmStatus Status { get; set; }

	/// <summary>Link to the item's underlying issue.</summary>
	public string? HtmlUrl { get; set; }

	/// <summary>Jira project key of the item's underlying issue.</summary>
	public string? ProjectKey { get; set; }

	/// <summary>The security category Codacy assigned to the item, for example <c>Cryptography</c>.</summary>
	public string? SecurityCategory { get; set; }

	/// <summary>
	/// The type of scan that identified the item: one of <c>SAST</c>, <c>SCA</c>, <c>ContainerSCA</c>,
	/// <c>Secrets</c>, <c>IaC</c>, <c>CICD</c>, <c>License</c>, <c>PenTesting</c>, <c>DAST</c> or <c>CSPM</c>.
	/// </summary>
	public string? ScanType { get; set; }

	/// <summary>Brief description of the issue. Specific to penetration testing issues.</summary>
	public string? Summary { get; set; }

	/// <summary>CVSS score. Specific to penetration testing issues.</summary>
	public float? CvssScore { get; set; }

	/// <summary>CVSS scoring vector. Specific to penetration testing issues.</summary>
	public string? CvssVector { get; set; }

	/// <summary>CWE software vulnerability identifier. Specific to penetration testing issues.</summary>
	public string? Cwe { get; set; }

	/// <summary>CVE identifier.</summary>
	public string? Cve { get; set; }

	/// <summary>The version in which this vulnerability was first detected.</summary>
	public string? AffectedVersion { get; set; }

	/// <summary>The versions (tag or commit SHA) in which this vulnerability was fixed.</summary>
	public List<string>? FixedVersion { get; set; }

	/// <summary>A URL identifying the affected application. Applicable to DAST findings.</summary>
	public string? Application { get; set; }

	/// <summary>Targets affected by the issue. Specific to penetration testing issues.</summary>
	public string? AffectedTargets { get; set; }

	/// <summary>Additional information about the issue.</summary>
	public string? AdditionalInfo { get; set; }

	/// <summary>Likelihood of exploitation. Specific to penetration testing issues.</summary>
	public string? Likelihood { get; set; }

	/// <summary>Effort required to fix. Specific to penetration testing issues.</summary>
	public string? EffortToFix { get; set; }

	/// <summary>Recommended steps to fix. Specific to penetration testing issues.</summary>
	public string? Remediation { get; set; }

	/// <summary>The target URLs that were scanned.</summary>
	public string? DastTargetUrls { get; set; }

	/// <summary>Name of the scanned container image.</summary>
	public string? ImageName { get; set; }

	/// <summary>Tag of the scanned container image.</summary>
	public string? ImageTag { get; set; }

	/// <summary>
	/// Ordered chains of package identifiers from the root package down to the vulnerable
	/// package. Only present for SCA findings.
	/// </summary>
	public List<List<string>>? DependencyChains { get; set; }

	/// <summary>Enrichment extracted from the associated advisory.</summary>
	public AdvisoryInformation? AdvisoryInformation { get; set; }
}

/// <summary>
/// Security and risk management item list, sorted by due date descending.
/// </summary>
public class SrmItemsResponse
{
	/// <summary>Security items.</summary>
	public required List<SrmItem> Data { get; set; }

	/// <summary>Pagination.</summary>
	public required PaginationInfo Pagination { get; set; }
}

/// <summary>
/// Response with a security and risk management item.
/// </summary>
public class SrmItemResponse
{
	/// <summary>Security item data.</summary>
	public required SrmItem Data { get; set; }
}

/// <summary>
/// Filter for container scanning items. The image <see cref="Name"/> is required;
/// <see cref="Tag"/> is optional. Filtering by tag alone is not allowed.
/// </summary>
public class ContainerImageFilter
{
	/// <summary>Container image name.</summary>
	public required string Name { get; set; }

	/// <summary>Container image tag.</summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? Tag { get; set; }
}

/// <summary>
/// Request body to filter the security issues of an organization.
/// </summary>
/// <remarks>
/// Codacy rejects explicit nulls on these filters, so every property is omitted when unset.
/// An unfiltered search therefore serializes to <c>{}</c>.
/// </remarks>
public class SearchSRMItems
{
	/// <summary>Repository names to filter by.</summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public List<string>? Repositories { get; set; }

	/// <summary>Severities to filter by.</summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public List<SrmPriority>? Priorities { get; set; }

	/// <summary>Statuses to filter by.</summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public List<SrmStatus>? Statuses { get; set; }

	/// <summary>
	/// Security categories to filter by. Use <c>_other_</c> to search for issues that have no
	/// security category.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public List<string>? Categories { get; set; }

	/// <summary>Scan types to filter by, for example <c>SAST</c> or <c>Secrets</c>.</summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public List<string>? ScanTypes { get; set; }

	/// <summary>Segment IDs to filter by.</summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public List<long>? Segments { get; set; }

	/// <summary>DAST target URLs to filter by.</summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public List<string>? DastTargetUrls { get; set; }

	/// <summary>Text to search for in security items.</summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? SearchText { get; set; }

	/// <summary>Container image to filter by.</summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public ContainerImageFilter? ContainerImage { get; set; }
}

/// <summary>
/// Request body with information about why an SRM item is being ignored.
/// </summary>
public class IgnoreSRMItemBody
{
	/// <summary>
	/// Why the issue is being ignored. One of <c>AcceptedUse</c>, <c>FalsePositive</c>,
	/// <c>NotExploitable</c>, <c>TestCode</c> or <c>ExternalCode</c>.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? Reason { get; set; }

	/// <summary>Comment describing why the issue is being ignored.</summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? Comment { get; set; }
}

/// <summary>
/// Metrics for the security and risk management dashboard.
/// </summary>
public class SRMDashboard
{
	/// <summary>Total open items.</summary>
	public required int TotalOpen { get; set; }

	/// <summary>Items opened this week.</summary>
	public required int TotalNewThisWeek { get; set; }

	/// <summary>Total closed items.</summary>
	public required int TotalClosed { get; set; }

	/// <summary>Open items within their due date.</summary>
	public required int OnTrack { get; set; }

	/// <summary>Open items approaching their due date.</summary>
	public required int DueSoon { get; set; }

	/// <summary>Open items past their due date.</summary>
	public required int Overdue { get; set; }

	/// <summary>Items closed within their due date.</summary>
	public required int ClosedOnTime { get; set; }

	/// <summary>Items closed after their due date.</summary>
	public required int ClosedLate { get; set; }

	/// <summary>Open items with Critical severity.</summary>
	public required int OpenCritical { get; set; }

	/// <summary>Open items with High severity.</summary>
	public required int OpenHigh { get; set; }

	/// <summary>Open items with Medium severity.</summary>
	public required int OpenMedium { get; set; }

	/// <summary>Open items with Low severity.</summary>
	public required int OpenLow { get; set; }

	/// <summary>Open items identified by a SAST scan.</summary>
	[JsonPropertyName("openSAST")]
	public required int OpenSast { get; set; }

	/// <summary>Open items identified by an SCA scan.</summary>
	[JsonPropertyName("openSCA")]
	public required int OpenSca { get; set; }

	/// <summary>Open items identified by a ContainerSCA scan.</summary>
	[JsonPropertyName("openContainerSCA")]
	public required int OpenContainerSca { get; set; }

	/// <summary>Open items identified by a Secrets scan.</summary>
	public required int OpenSecrets { get; set; }

	/// <summary>Open items identified by an IaC scan.</summary>
	[JsonPropertyName("openIaC")]
	public required int OpenIaC { get; set; }

	/// <summary>Open items identified by a CI/CD scan.</summary>
	[JsonPropertyName("openCICD")]
	public required int OpenCicd { get; set; }

	/// <summary>Open items identified by a License scan.</summary>
	public required int OpenLicense { get; set; }

	/// <summary>Open items identified by penetration testing.</summary>
	public required int OpenPenTesting { get; set; }

	/// <summary>Open items identified by a DAST scan.</summary>
	[JsonPropertyName("openDAST")]
	public required int OpenDast { get; set; }

	/// <summary>Open items identified by a CSPM scan.</summary>
	[JsonPropertyName("openCSPM")]
	public required int OpenCspm { get; set; }

	/// <summary>Open items with no attributed scan type.</summary>
	public required int OpenScanNotAttributed { get; set; }
}

/// <summary>
/// Response carrying the metrics for the security and risk management dashboard.
/// </summary>
public class SRMDashboardResponse
{
	/// <summary>Dashboard data.</summary>
	public required SRMDashboard Data { get; set; }
}

/// <summary>
/// Request body to filter the metrics of an organization's security issues dashboard.
/// </summary>
public class SearchSRMDashboard
{
	/// <summary>Repository names to filter by.</summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public List<string>? Repositories { get; set; }

	/// <summary>Severities to filter by.</summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public List<SrmPriority>? Priorities { get; set; }

	/// <summary>Security categories to filter by.</summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public List<string>? Categories { get; set; }

	/// <summary>Scan types to filter by.</summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public List<string>? ScanTypes { get; set; }

	/// <summary>Segment IDs to filter by.</summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public List<long>? Segments { get; set; }
}

/// <summary>
/// Request body to filter the list of organization repositories with security findings.
/// </summary>
public class SearchSRMDashboardRepositories
{
	/// <summary>Repository names to filter by.</summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public List<string>? Repositories { get; set; }

	/// <summary>Segment IDs to filter by.</summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public List<long>? Segments { get; set; }
}

/// <summary>
/// Open issue counts for a repository, aggregated by severity.
/// </summary>
public class SRMRepositoryIssueCount
{
	/// <summary>Codacy repository ID.</summary>
	public required long Id { get; set; }

	/// <summary>Repository name.</summary>
	public required string Name { get; set; }

	/// <summary>Number of open issues with Critical severity.</summary>
	public required int Critical { get; set; }

	/// <summary>Number of open issues with High severity.</summary>
	public required int High { get; set; }

	/// <summary>Number of open issues with Medium severity.</summary>
	public required int Medium { get; set; }

	/// <summary>Number of open issues with Low severity.</summary>
	public required int Low { get; set; }
}

/// <summary>
/// List of repositories with their respective issue counts.
/// </summary>
public class SRMDashboardRepositoriesResponse
{
	/// <summary>Repository data.</summary>
	public required List<SRMRepositoryIssueCount> Data { get; set; }
}

/// <summary>
/// Request body to filter the evolution of security findings over time.
/// </summary>
public class SearchSRMDashboardHistory
{
	/// <summary>Repository names to filter by.</summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public List<string>? Repositories { get; set; }

	/// <summary>Segment IDs to filter by.</summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public List<long>? Segments { get; set; }
}

/// <summary>
/// Security finding counts over one time interval.
/// </summary>
public class SRMHistoryDataPoint
{
	/// <summary>Beginning of the time interval (inclusive).</summary>
	public required DateTimeOffset Since { get; set; }

	/// <summary>End of the time interval (exclusive).</summary>
	public required DateTimeOffset Until { get; set; }

	/// <summary>Critical findings introduced during the interval.</summary>
	public required int NewCritical { get; set; }

	/// <summary>High findings introduced during the interval.</summary>
	public required int NewHigh { get; set; }

	/// <summary>Medium findings introduced during the interval.</summary>
	public required int NewMedium { get; set; }

	/// <summary>Low findings introduced during the interval.</summary>
	public required int NewLow { get; set; }

	/// <summary>Critical findings fixed during the interval.</summary>
	public required int FixedCritical { get; set; }

	/// <summary>High findings fixed during the interval.</summary>
	public required int FixedHigh { get; set; }

	/// <summary>Medium findings fixed during the interval.</summary>
	public required int FixedMedium { get; set; }

	/// <summary>Low findings fixed during the interval.</summary>
	public required int FixedLow { get; set; }

	/// <summary>Critical findings open at the beginning of the interval.</summary>
	public required int OpenCritical { get; set; }

	/// <summary>High findings open at the beginning of the interval.</summary>
	public required int OpenHigh { get; set; }

	/// <summary>Medium findings open at the beginning of the interval.</summary>
	public required int OpenMedium { get; set; }

	/// <summary>Low findings open at the beginning of the interval.</summary>
	public required int OpenLow { get; set; }

	/// <summary>Critical findings ignored at the beginning of the interval.</summary>
	public required int IgnoredCritical { get; set; }

	/// <summary>High findings ignored at the beginning of the interval.</summary>
	public required int IgnoredHigh { get; set; }

	/// <summary>Medium findings ignored at the beginning of the interval.</summary>
	public required int IgnoredMedium { get; set; }

	/// <summary>Low findings ignored at the beginning of the interval.</summary>
	public required int IgnoredLow { get; set; }

	/// <summary>Critical findings unignored at the beginning of the interval.</summary>
	public required int UnignoredCritical { get; set; }

	/// <summary>High findings unignored at the beginning of the interval.</summary>
	public required int UnignoredHigh { get; set; }

	/// <summary>Medium findings unignored at the beginning of the interval.</summary>
	public required int UnignoredMedium { get; set; }

	/// <summary>Low findings unignored at the beginning of the interval.</summary>
	public required int UnignoredLow { get; set; }
}

/// <summary>
/// Evolution of security findings over time.
/// </summary>
public class SRMDashboardHistoryResponse
{
	/// <summary>History data.</summary>
	public required List<SRMHistoryDataPoint> Data { get; set; }
}

/// <summary>
/// Request body to filter the security categories of an organization.
/// </summary>
public class SearchSRMDashboardCategories
{
	/// <summary>Repository names to filter by.</summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public List<string>? Repositories { get; set; }

	/// <summary>Segment IDs to filter by.</summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public List<long>? Segments { get; set; }
}

/// <summary>
/// Open issue counts for a security category.
/// </summary>
public class SRMCategoryIssueCount
{
	/// <summary>Security category name. <c>_other_</c> covers issues with no category.</summary>
	public required string Name { get; set; }

	/// <summary>Total open issues for the category.</summary>
	public required int Total { get; set; }
}

/// <summary>
/// List of security categories with their respective issue counts.
/// </summary>
public class SRMDashboardCategoriesResponse
{
	/// <summary>Category data.</summary>
	public required List<SRMCategoryIssueCount> Data { get; set; }
}

/// <summary>
/// Processing state of a DAST report.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<SrmDastReportState>))]
public enum SrmDastReportState
{
	/// <summary>Still being processed.</summary>
	InProgress,

	/// <summary>Processed successfully.</summary>
	Success,

	/// <summary>Failed to process.</summary>
	Failure
}

/// <summary>
/// The DAST scanning tool that generated a report.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<DastTool>))]
public enum DastTool
{
	/// <summary>OWASP ZAP.</summary>
	[JsonStringEnumMemberName("ZAP")]
	Zap
}

/// <summary>
/// Metadata identifying an uploaded DAST report.
/// </summary>
public class SRMDASTReportUploadResponse
{
	/// <summary>Unique identifier of the uploaded report.</summary>
	public required string Id { get; set; }
}

/// <summary>
/// A dynamic application security testing (DAST) report.
/// </summary>
public class SRMDastReport
{
	/// <summary>The report's internal ID.</summary>
	public required Guid Id { get; set; }

	/// <summary>Codacy's organization identifier.</summary>
	public required long OrganizationId { get; set; }

	/// <summary>When the report was created on Codacy's platform.</summary>
	public required DateTimeOffset CreatedAt { get; set; }

	/// <summary>When the report was last updated on Codacy's platform.</summary>
	public required DateTimeOffset UpdatedAt { get; set; }

	/// <summary>When the report was originally generated.</summary>
	public required DateTimeOffset GeneratedAt { get; set; }

	/// <summary>The report's current processing state.</summary>
	public required SrmDastReportState State { get; set; }

	/// <summary>The DAST scanning tool used to generate the report.</summary>
	public required DastTool Tool { get; set; }

	/// <summary>
	/// Why the report failed to be processed. Only set when <see cref="State"/> is
	/// <see cref="SrmDastReportState.Failure"/>.
	/// </summary>
	public string? FailureReason { get; set; }
}

/// <summary>
/// A page of DAST reports submitted to Codacy.
/// </summary>
public class SRMDastReportResponse
{
	/// <summary>DAST reports.</summary>
	public required List<SRMDastReport> Data { get; set; }

	/// <summary>Pagination.</summary>
	public PaginationInfo? Pagination { get; set; }
}

/// <summary>
/// Organization admin or security manager.
/// </summary>
public class SecurityManager
{
	/// <summary>User ID internal to Codacy.</summary>
	public required long UserId { get; set; }

	/// <summary>The user's name.</summary>
	public string? Name { get; set; }

	/// <summary>The user's email address.</summary>
	public required string Email { get; set; }

	/// <summary>When the user was made a security manager.</summary>
	public required DateTimeOffset CreatedAt { get; set; }
}

/// <summary>
/// Security manager list, sorted by organization admin status and then alphabetically.
/// </summary>
public class SecurityManagersResponse
{
	/// <summary>Security managers.</summary>
	public required List<SecurityManager> Data { get; set; }

	/// <summary>Pagination.</summary>
	public required PaginationInfo Pagination { get; set; }
}

/// <summary>
/// Assign or revoke the security manager role for an organization member.
/// </summary>
public class SecurityManagerBody
{
	/// <summary>User ID of the organization member.</summary>
	public required long UserId { get; set; }
}

/// <summary>
/// List of repositories that have security issues.
/// </summary>
public class SecurityRepositoriesResponse
{
	/// <summary>Repositories with security issues.</summary>
	public required List<RepositorySummary> Data { get; set; }

	/// <summary>Pagination.</summary>
	public required PaginationInfo Pagination { get; set; }
}

/// <summary>
/// List of security categories that have security issues.
/// </summary>
public class SecurityCategoriesResponse
{
	/// <summary>Security category names.</summary>
	public required List<string> Data { get; set; }

	/// <summary>Pagination.</summary>
	public required PaginationInfo Pagination { get; set; }
}

/// <summary>
/// SLA configuration of an organization, in days per severity.
/// </summary>
public class SLAConfig
{
	/// <summary>Days allowed to handle Critical severity security issues.</summary>
	public required int CriticalSla { get; set; }

	/// <summary>Days allowed to handle High severity security issues.</summary>
	public required int HighSla { get; set; }

	/// <summary>Days allowed to handle Medium severity security issues.</summary>
	public required int MediumSla { get; set; }

	/// <summary>Days allowed to handle Low severity security issues.</summary>
	public required int LowSla { get; set; }
}

/// <summary>
/// Response body for getting an organization's SLA configuration.
/// </summary>
public class SLAConfigResponse
{
	/// <summary>SLA configuration.</summary>
	public required SLAConfig SlaConfig { get; set; }
}

/// <summary>
/// Request body for updating an SLA configuration.
/// </summary>
public class SLAConfigBody
{
	/// <summary>SLA configuration.</summary>
	public required SLAConfig SlaConfig { get; set; }
}

/// <summary>
/// The severity of an OSSF Scorecard check.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<OssfScorecardSeverity>))]
public enum OssfScorecardSeverity
{
	/// <summary>Critical severity.</summary>
	Critical,

	/// <summary>High severity.</summary>
	High,

	/// <summary>Medium severity.</summary>
	Medium,

	/// <summary>Low severity.</summary>
	Low,

	/// <summary>Severity not known.</summary>
	Unknown
}

/// <summary>
/// Documentation for an OSSF Scorecard check.
/// </summary>
public class OssfScorecardDocumentation
{
	/// <summary>The URL of the documentation.</summary>
	public required string Url { get; set; }

	/// <summary>The short documentation of the check.</summary>
	[JsonPropertyName("short")]
	public required string ShortDescription { get; set; }
}

/// <summary>
/// An OSSF Scorecard check.
/// </summary>
public class OssfScorecardCheck
{
	/// <summary>The name of the check.</summary>
	public required string Name { get; set; }

	/// <summary>The score of the check.</summary>
	public required float Score { get; set; }

	/// <summary>The reason for the score.</summary>
	public required string Reason { get; set; }

	/// <summary>The details of the check.</summary>
	public required List<string> Details { get; set; }

	/// <summary>The documentation of the check.</summary>
	public required OssfScorecardDocumentation Documentation { get; set; }

	/// <summary>The severity of the check.</summary>
	public required OssfScorecardSeverity Severity { get; set; }
}

/// <summary>
/// OSSF Scorecard information for a repository.
/// </summary>
public class OssfScorecard
{
	/// <summary>The overall OSSF Scorecard score.</summary>
	public required float Score { get; set; }

	/// <summary>The date of the scorecard.</summary>
	public required string Date { get; set; }

	/// <summary>The list of OSSF Scorecard checks.</summary>
	public required List<OssfScorecardCheck> Checks { get; set; }

	/// <summary>The number of failing checks.</summary>
	public required int FailingCheckCount { get; set; }

	/// <summary>The number of passing checks.</summary>
	public required int PassingCheckCount { get; set; }
}

/// <summary>
/// Response body carrying OSSF Scorecard information.
/// </summary>
public class OssfScorecardResponse
{
	/// <summary>Scorecard data.</summary>
	public required OssfScorecard Data { get; set; }
}

/// <summary>
/// Request body to fetch OSSF Scorecard information, by repository URL or dependency PURL.
/// </summary>
public class OssfScorecardUrlRequest
{
	/// <summary>The URL of the repository to fetch OSSF Scorecard information for.</summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? Url { get; set; }

	/// <summary>The PURL of the dependency to fetch OSSF Scorecard information for.</summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? Purl { get; set; }
}
