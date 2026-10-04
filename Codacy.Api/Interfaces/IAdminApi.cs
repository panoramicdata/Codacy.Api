using System.Text.Json;
using Codacy.Api.Models;
using Refit;

namespace Codacy.Api.Interfaces;

/// <summary>
/// Interface for Codacy admin API operations (Codacy admins only)
/// </summary>
public interface IAdminApi
{
	/// <summary>
	/// Search for an entity like Organization or Repository, supports ids and names
	/// </summary>
	[Get("/api/v3/admin")]
	Task<AdminEntityGroupResponse> AdminSearchAsync(
		[Query] string? search,
		CancellationToken cancellationToken);

	/// <summary>
	/// Returns the requested admin entity
	/// </summary>
	[Get("/api/v3/admin/{adminEntityGroupSlug}/{adminEntityIdentifier}")]
	Task<AdminEntityResponse> GetAdminEntityAsync(
		string adminEntityGroupSlug,
		long adminEntityIdentifier,
		CancellationToken cancellationToken);

	/// <summary>
	/// Returns the requested resources of a given admin entity
	/// </summary>
	[Get("/api/v3/admin/{adminEntityGroupSlug}/{adminEntityIdentifier}/{adminResourceSlug}")]
	Task<ListResponse<AdminEntityResource>> ListAdminEntityResourcesAsync(
		string adminEntityGroupSlug,
		long adminEntityIdentifier,
		string adminResourceSlug,
		[Query] string? cursor,
		[Query] int? limit,
		CancellationToken cancellationToken);

	/// <summary>
	/// Executes an action on a given admin entity; the payload fields match the action metadata
	/// </summary>
	[Post("/api/v3/admin/{adminEntityGroupSlug}/{adminEntityIdentifier}/actions/{adminActionSlug}")]
	Task<AdminEntityActionResponse> ExecuteAdminActionAsync(
		string adminEntityGroupSlug,
		long adminEntityIdentifier,
		string adminActionSlug,
		[Body] Dictionary<string, JsonElement> payload,
		CancellationToken cancellationToken);

	/// <summary>
	/// Generates a license for self-hosted instances of Codacy
	/// </summary>
	[Post("/api/v3/admin/license")]
	Task<LicenseResponse> GenerateLicenseAsync(
		[Body] License body,
		CancellationToken cancellationToken);

	/// <summary>
	/// Delete Codacy users based on a CSV file exported by GitHub Enterprise (sent as plain text)
	/// </summary>
	[Delete("/api/v3/admin/dormantAccounts")]
	Task<DeleteDormantAccountsResponse> DeleteDormantAccountsAsync(
		[Body] string csv,
		CancellationToken cancellationToken);

	/// <summary>
	/// Upload pen test reports for an organization (the provider is the provider code, for example gh)
	/// </summary>
	[Multipart]
	[Post("/api/v3/admin/security/penTest/reports")]
	Task UploadPenTestReportAsync(
		[AliasAs("csvdata")] StreamPart csvData,
		[AliasAs("provider")] string provider,
		[AliasAs("organizationName")] string organizationName,
		CancellationToken cancellationToken);
}
