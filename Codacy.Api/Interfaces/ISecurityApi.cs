using Codacy.Api.Models;
using Refit;

namespace Codacy.Api.Interfaces;

/// <summary>
/// Interface for Security and Risk Management API operations
/// </summary>
public interface ISecurityApi
{
	/// <summary>
	/// Search security items for an organization
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{remoteOrganizationName}/security/items/search")]
	Task<SrmItemsResponse> SearchSecurityItemsAsync(
		Provider provider,
		string remoteOrganizationName,
		[Body] SearchSRMItems? body,
		[Query] string? cursor,
		[Query] int? limit,
		[Query] string? sort,
		[Query] string? direction,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get a security item
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{remoteOrganizationName}/security/items/{srmItemId}")]
	Task<SrmItemResponse> GetSecurityItemAsync(
		Provider provider,
		string remoteOrganizationName,
		Guid srmItemId,
		CancellationToken cancellationToken);

	/// <summary>
	/// Ignore a security item
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{remoteOrganizationName}/security/items/{srmItemId}/ignore")]
	Task<SrmItemResponse> IgnoreSecurityItemAsync(
		Provider provider,
		string remoteOrganizationName,
		Guid srmItemId,
		[Body] IgnoreSRMItemBody body,
		CancellationToken cancellationToken);

	/// <summary>
	/// Unignore a security item
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{remoteOrganizationName}/security/items/{srmItemId}/unignore")]
	Task<SrmItemResponse> UnignoreSecurityItemAsync(
		Provider provider,
		string remoteOrganizationName,
		Guid srmItemId,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get security dashboard metrics
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{remoteOrganizationName}/security/dashboard")]
	Task<SRMDashboardResponse> SearchSecurityDashboardAsync(
		Provider provider,
		string remoteOrganizationName,
		[Body] SearchSRMDashboard? body,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get security dashboard repositories
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{remoteOrganizationName}/security/dashboard/repositories/search")]
	Task<SRMDashboardRepositoriesResponse> SearchSecurityDashboardRepositoriesAsync(
		Provider provider,
		string remoteOrganizationName,
		[Body] SearchSRMDashboardRepositories? body,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get security dashboard history
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{remoteOrganizationName}/security/dashboard/history/search")]
	Task<SRMDashboardHistoryResponse> SearchSecurityDashboardHistoryAsync(
		Provider provider,
		string remoteOrganizationName,
		[Body] SearchSRMDashboardHistory? body,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get security dashboard categories
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{remoteOrganizationName}/security/dashboard/categories/search")]
	Task<SRMDashboardCategoriesResponse> SearchSecurityDashboardCategoriesAsync(
		Provider provider,
		string remoteOrganizationName,
		[Body] SearchSRMDashboardCategories? body,
		CancellationToken cancellationToken);

	/// <summary>
	/// List security managers
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{remoteOrganizationName}/security/managers")]
	Task<SecurityManagersResponse> ListSecurityManagersAsync(
		Provider provider,
		string remoteOrganizationName,
		[Query] string? cursor,
		[Query] int? limit,
		CancellationToken cancellationToken);

	/// <summary>
	/// Add a security manager
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{remoteOrganizationName}/security/managers")]
	Task PostSecurityManagerAsync(
		Provider provider,
		string remoteOrganizationName,
		[Body] SecurityManagerBody body,
		CancellationToken cancellationToken);

	/// <summary>
	/// Remove a security manager
	/// </summary>
	[Delete("/api/v3/organizations/{provider}/{remoteOrganizationName}/security/managers/{userId}")]
	Task DeleteSecurityManagerAsync(
		Provider provider,
		string remoteOrganizationName,
		long userId,
		CancellationToken cancellationToken);

	/// <summary>
	/// List repositories with security issues
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{remoteOrganizationName}/security/repositories")]
	Task<SecurityRepositoriesResponse> ListSecurityRepositoriesAsync(
		Provider provider,
		string remoteOrganizationName,
		[Query] string? cursor,
		[Query] int? limit,
		[Query] string? segments,
		CancellationToken cancellationToken);

	/// <summary>
	/// List security categories with findings
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{remoteOrganizationName}/security/categories")]
	Task<SecurityCategoriesResponse> ListSecurityCategoriesAsync(
		Provider provider,
		string remoteOrganizationName,
		[Query] string? cursor,
		[Query] int? limit,
		CancellationToken cancellationToken);

	/// <summary>
	/// Upload DAST report
	/// </summary>
	[Multipart]
	[Post("/api/v3/organizations/{provider}/{remoteOrganizationName}/security/tools/dast/{toolName}/reports")]
	Task<SRMDASTReportUploadResponse> UploadDASTReportAsync(
		Provider provider,
		string remoteOrganizationName,
		string toolName,
		[AliasAs("file")] StreamPart file,
		[AliasAs("reportFormat")] string reportFormat,
		CancellationToken cancellationToken);

	/// <summary>
	/// List DAST reports
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{remoteOrganizationName}/security/dast/reports")]
	Task<SRMDastReportResponse> ListDastReportsAsync(
		Provider provider,
		string remoteOrganizationName,
		[Query] string? cursor,
		[Query] int? limit,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get SLA configuration
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{remoteOrganizationName}/security/sla")]
	Task<SLAConfigResponse> GetSLAConfigAsync(
		Provider provider,
		string remoteOrganizationName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Update SLA configuration
	/// </summary>
	[Put("/api/v3/organizations/{provider}/{remoteOrganizationName}/security/sla")]
	Task<SLAConfigResponse> UpdateSLAConfigAsync(
		Provider provider,
		string remoteOrganizationName,
		[Body] SLAConfigBody body,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get OSSF Scorecard for a repository
	/// </summary>
	[Post("/api/v3/security/dependencies/ossf/scorecard")]
	Task<OssfScorecardResponse> GetOssfScorecardAsync(
		[Body] OssfScorecardUrlRequest request,
		CancellationToken cancellationToken);
}
