using Codacy.Api.Models;
using Refit;

namespace Codacy.Api.Interfaces;

/// <summary>
/// Interface for Gate Policies API operations
/// </summary>
public interface IGatePoliciesApi
{
	/// <summary>
	/// List the gate policies for an organization
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/gate-policies")]
	Task<GatePoliciesListResponse> ListGatePoliciesAsync(
		Provider provider,
		string organizationName,
		[Query] string? cursor,
		[Query] int? limit,
		CancellationToken cancellationToken);

	/// <summary>
	/// Create a gate policy
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{organizationName}/gate-policies")]
	Task<GetGatePolicyResultResponse> CreateGatePolicyAsync(
		Provider provider,
		string organizationName,
		[Body] CreateGatePolicyBody body,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get a gate policy
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/gate-policies/{gatePolicyId}")]
	Task<GetGatePolicyResultResponse> GetGatePolicyAsync(
		Provider provider,
		string organizationName,
		long gatePolicyId,
		CancellationToken cancellationToken);

	/// <summary>
	/// Update a gate policy
	/// </summary>
	[Patch("/api/v3/organizations/{provider}/{organizationName}/gate-policies/{gatePolicyId}")]
	Task<GetGatePolicyResultResponse> UpdateGatePolicyAsync(
		Provider provider,
		string organizationName,
		long gatePolicyId,
		[Body] UpdateGatePolicyBody body,
		CancellationToken cancellationToken);

	/// <summary>
	/// Delete a gate policy
	/// </summary>
	[Delete("/api/v3/organizations/{provider}/{organizationName}/gate-policies/{gatePolicyId}")]
	Task DeleteGatePolicyAsync(
		Provider provider,
		string organizationName,
		long gatePolicyId,
		CancellationToken cancellationToken);

	/// <summary>
	/// Set the gate policy as the default for an organization
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{organizationName}/gate-policies/{gatePolicyId}/setDefault")]
	Task SetDefaultGatePolicyAsync(
		Provider provider,
		string organizationName,
		long gatePolicyId,
		CancellationToken cancellationToken);

	/// <summary>
	/// Set the built-in Codacy gate policy as the default for an organization
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{organizationName}/gate-policies/setCodacyDefault")]
	Task SetCodacyDefaultGatePolicyAsync(
		Provider provider,
		string organizationName,
		CancellationToken cancellationToken);

	/// <summary>
	/// List all repositories following a gate policy
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/gate-policies/{gatePolicyId}/repositories")]
	Task<ListResponse<RepositoryIdentification>> ListRepositoriesFollowingGatePolicyAsync(
		Provider provider,
		string organizationName,
		long gatePolicyId,
		[Query] string? cursor,
		[Query] int? limit,
		CancellationToken cancellationToken);

	/// <summary>
	/// Link or unlink a gate policy to a list of repositories
	/// </summary>
	[Put("/api/v3/organizations/{provider}/{organizationName}/gate-policies/{gatePolicyId}/repositories")]
	Task ApplyGatePolicyToRepositoriesAsync(
		Provider provider,
		string organizationName,
		long gatePolicyId,
		[Body] ApplyGatePolicyToRepositoriesBody body,
		CancellationToken cancellationToken);

	/// <summary>
	/// Create a compliance standard for an organization
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{organizationName}/compliance-standards")]
	Task<CodingStandardResponse> CreateComplianceStandardAsync(
		Provider provider,
		string organizationName,
		[Body] CreateComplianceStandardBody body,
		CancellationToken cancellationToken);
}
