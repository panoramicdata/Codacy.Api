using Codacy.Api.Models;
using Refit;

namespace Codacy.Api.Interfaces;

/// <summary>
/// Interface for Coding Standards API operations
/// </summary>
public interface ICodingStandardsApi
{
	/// <summary>
	/// List coding standards for an organization
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{remoteOrganizationName}/coding-standards")]
	Task<CodingStandardsListResponse> ListCodingStandardsAsync(
		Provider provider,
		string remoteOrganizationName,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Create a coding standard
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{remoteOrganizationName}/coding-standards")]
	Task<CodingStandardResponse> CreateCodingStandardAsync(
		Provider provider,
		string remoteOrganizationName,
		[Body] CreateCodingStandardBody body,
		[Query] string? sourceRepository = null,
		[Query] long? sourceCodingStandard = null,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Create a coding standard from presets
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{remoteOrganizationName}/presets-standards")]
	Task<CodingStandardResponse> CreateCodingStandardPresetAsync(
		Provider provider,
		string remoteOrganizationName,
		[Body] CreateCodingStandardPresetBody body,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Get a coding standard
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{remoteOrganizationName}/coding-standards/{codingStandardId}")]
	Task<CodingStandardResponse> GetCodingStandardAsync(
		Provider provider,
		string remoteOrganizationName,
		long codingStandardId,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Delete a coding standard
	/// </summary>
	[Delete("/api/v3/organizations/{provider}/{remoteOrganizationName}/coding-standards/{codingStandardId}")]
	Task DeleteCodingStandardAsync(
		Provider provider,
		string remoteOrganizationName,
		long codingStandardId,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Duplicate a coding standard
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{remoteOrganizationName}/coding-standards/{codingStandardId}/duplicate")]
	Task<CodingStandardResponse> DuplicateCodingStandardAsync(
		Provider provider,
		string remoteOrganizationName,
		long codingStandardId,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Set a coding standard as default
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{remoteOrganizationName}/coding-standards/{codingStandardId}/setDefault")]
	Task SetDefaultCodingStandardAsync(
		Provider provider,
		string remoteOrganizationName,
		long codingStandardId,
		[Body] SetDefaultCodingStandardBody body,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// List tools in a coding standard
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{remoteOrganizationName}/coding-standards/{codingStandardId}/tools")]
	Task<CodingStandardToolsListResponse> ListCodingStandardToolsAsync(
		Provider provider,
		string remoteOrganizationName,
		long codingStandardId,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// List patterns for a tool in a coding standard
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{remoteOrganizationName}/coding-standards/{codingStandardId}/tools/{toolUuid}/patterns")]
	Task<ConfiguredPatternsListResponse> ListCodingStandardPatternsAsync(
		Provider provider,
		string remoteOrganizationName,
		long codingStandardId,
		string toolUuid,
		[Query] string? languages = null,
		[Query] string? categories = null,
		[Query] string? severityLevels = null,
		[Query] string? tags = null,
		[Query] string? search = null,
		[Query] bool? enabled = null,
		[Query] bool? recommended = null,
		[Query] string? sort = null,
		[Query] string? direction = null,
		[Query] string? cursor = null,
		[Query] int? limit = null,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Update patterns for a tool in a coding standard
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{remoteOrganizationName}/coding-standards/{codingStandardId}/tools/{toolUuid}/patterns/update")]
	Task UpdateCodingStandardPatternsAsync(
		Provider provider,
		string remoteOrganizationName,
		long codingStandardId,
		string toolUuid,
		[Body] UpdatePatternsBody body,
		[Query] string? languages = null,
		[Query] string? categories = null,
		[Query] string? severityLevels = null,
		[Query] string? tags = null,
		[Query] string? search = null,
		[Query] bool? recommended = null,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Update tool configuration in a coding standard
	/// </summary>
	[Patch("/api/v3/organizations/{provider}/{remoteOrganizationName}/coding-standards/{codingStandardId}/tools/{toolUuid}")]
	Task UpdateCodingStandardToolConfigurationAsync(
		Provider provider,
		string remoteOrganizationName,
		long codingStandardId,
		string toolUuid,
		[Body] ToolConfiguration body,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// List repositories using a coding standard
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{remoteOrganizationName}/coding-standards/{codingStandardId}/repositories")]
	Task<CodingStandardRepositoriesListResponse> ListCodingStandardRepositoriesAsync(
		Provider provider,
		string remoteOrganizationName,
		long codingStandardId,
		[Query] string? cursor = null,
		[Query] int? limit = null,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Apply a coding standard to repositories
	/// </summary>
	[Patch("/api/v3/organizations/{provider}/{remoteOrganizationName}/coding-standards/{codingStandardId}/repositories")]
	Task<ApplyCodingStandardToRepositoriesResultResponse> ApplyCodingStandardToRepositoriesAsync(
		Provider provider,
		string remoteOrganizationName,
		long codingStandardId,
		[Body] ApplyCodingStandardToRepositoriesBody body,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Promote a draft coding standard
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{remoteOrganizationName}/coding-standards/{codingStandardId}/promote")]
	Task<ApplyCodingStandardToRepositoriesResultResponse> PromoteDraftCodingStandardAsync(
		Provider provider,
		string remoteOrganizationName,
		long codingStandardId,
		CancellationToken cancellationToken = default);
}
