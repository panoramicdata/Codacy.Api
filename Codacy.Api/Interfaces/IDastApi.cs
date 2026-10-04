using Codacy.Api.Models;
using Refit;

namespace Codacy.Api.Interfaces;

/// <summary>
/// Interface for DAST API operations
/// </summary>
public interface IDastApi
{
	/// <summary>
	/// List configured DAST targets
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/dast/targets")]
	Task<ListResponse<DastTarget>> GetDastTargetsAsync(
		Provider provider,
		string organizationName,
		[Query] string? cursor,
		[Query] int? limit,
		CancellationToken cancellationToken);

	/// <summary>
	/// Create a DAST target
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{organizationName}/dast/targets")]
	Task<DastTargetResponse> CreateDastTargetAsync(
		Provider provider,
		string organizationName,
		[Body] CreateDastTargetBody body,
		CancellationToken cancellationToken);

	/// <summary>
	/// Delete a DAST target
	/// </summary>
	[Delete("/api/v3/organizations/{provider}/{organizationName}/dast/targets/{dastTargetId}")]
	Task DeleteDastTargetAsync(
		Provider provider,
		string organizationName,
		long dastTargetId,
		CancellationToken cancellationToken);

	/// <summary>
	/// Enqueue a DAST analysis for the given target
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{organizationName}/dast/targets/{dastTargetId}/analyze")]
	Task<AnalyzeDastTargetResponse> AnalyzeDastTargetAsync(
		Provider provider,
		string organizationName,
		long dastTargetId,
		CancellationToken cancellationToken);
}
