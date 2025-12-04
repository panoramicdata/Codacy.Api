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
		CancellationToken cancellationToken);

	/// <summary>
	/// Create a coding standard
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{remoteOrganizationName}/coding-standards")]
	Task<CodingStandardResponse> CreateCodingStandardAsync(
		Provider provider,
		string remoteOrganizationName,
		[Body] CreateCodingStandardBody body,
		[Query] string? sourceRepository,
		[Query] long? sourceCodingStandard,
		CancellationToken cancellationToken);

	/// <summary>
	/// Create a coding standard from presets
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{remoteOrganizationName}/presets-standards")]
	Task<CodingStandardResponse> CreateCodingStandardPresetAsync(
		Provider provider,
		string remoteOrganizationName,
		[Body] CreateCodingStandardPresetBody body,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get a coding standard
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{remoteOrganizationName}/coding-standards/{codingStandardId}")]
	Task<CodingStandardResponse> GetCodingStandardAsync(
		Provider provider,
		string remoteOrganizationName,
		long codingStandardId,
		CancellationToken cancellationToken);

	/// <summary>
	/// Delete a coding standard
	/// </summary>
	[Delete("/api/v3/organizations/{provider}/{remoteOrganizationName}/coding-standards/{codingStandardId}")]
	Task DeleteCodingStandardAsync(
		Provider provider,
		string remoteOrganizationName,
		long codingStandardId,
		CancellationToken cancellationToken);

	/// <summary>
	/// Duplicate a coding standard
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{remoteOrganizationName}/coding-standards/{codingStandardId}/duplicate")]
	Task<CodingStandardResponse> DuplicateCodingStandardAsync(
		Provider provider,
		string remoteOrganizationName,
		long codingStandardId,
		CancellationToken cancellationToken);

	/// <summary>
	/// Set a coding standard as default
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{remoteOrganizationName}/coding-standards/{codingStandardId}/setDefault")]
	Task SetDefaultCodingStandardAsync(
		Provider provider,
		string remoteOrganizationName,
		long codingStandardId,
		[Body] SetDefaultCodingStandardBody body,
		CancellationToken cancellationToken);

	/// <summary>
	/// List tools in a coding standard
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{remoteOrganizationName}/coding-standards/{codingStandardId}/tools")]
	Task<CodingStandardToolsListResponse> ListCodingStandardToolsAsync(
		Provider provider,
		string remoteOrganizationName,
		long codingStandardId,
		CancellationToken cancellationToken);

	/// <summary>
	/// List patterns for a tool in a coding standard
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{remoteOrganizationName}/coding-standards/{codingStandardId}/tools/{toolUuid}/patterns")]
	Task<ConfiguredPatternsListResponse> ListCodingStandardPatternsAsync(
		Provider provider,
		string remoteOrganizationName,
		long codingStandardId,
		string toolUuid,
		[Query] string? languages,
		[Query] string? categories,
		[Query] string? severityLevels,
		[Query] string? tags,
		[Query] string? search,
		[Query] bool? enabled,
		[Query] bool? recommended,
		[Query] string? sort,
		[Query] string? direction,
		[Query] string? cursor,
		[Query] int? limit,
		CancellationToken cancellationToken);

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
		[Query] string? languages,
		[Query] string? categories,
		[Query] string? severityLevels,
		[Query] string? tags,
		[Query] string? search,
		[Query] bool? recommended,
		CancellationToken cancellationToken);

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
		CancellationToken cancellationToken);

	/// <summary>
	/// List repositories using a coding standard
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{remoteOrganizationName}/coding-standards/{codingStandardId}/repositories")]
	Task<CodingStandardRepositoriesListResponse> ListCodingStandardRepositoriesAsync(
		Provider provider,
		string remoteOrganizationName,
		long codingStandardId,
		[Query] string? cursor,
		[Query] int? limit,
		CancellationToken cancellationToken);

	/// <summary>
	/// Apply a coding standard to repositories
	/// </summary>
	[Patch("/api/v3/organizations/{provider}/{remoteOrganizationName}/coding-standards/{codingStandardId}/repositories")]
	Task<ApplyCodingStandardToRepositoriesResultResponse> ApplyCodingStandardToRepositoriesAsync(
		Provider provider,
		string remoteOrganizationName,
		long codingStandardId,
		[Body] ApplyCodingStandardToRepositoriesBody body,
		CancellationToken cancellationToken);

	/// <summary>
	/// Promote a draft coding standard
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{remoteOrganizationName}/coding-standards/{codingStandardId}/promote")]
	Task<ApplyCodingStandardToRepositoriesResultResponse> PromoteDraftCodingStandardAsync(
		Provider provider,
		string remoteOrganizationName,
		long codingStandardId,
		CancellationToken cancellationToken);
}
