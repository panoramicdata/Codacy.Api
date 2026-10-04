using Codacy.Api.Models;
using Refit;

namespace Codacy.Api.Interfaces;

/// <summary>
/// Interface for AI inventory API operations (experimental upstream)
/// </summary>
public interface IAiInventoryApi
{
	/// <summary>
	/// List AI inventory provider summaries for an organization
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{organizationName}/ai-inventory/providers/summaries/search")]
	Task<ListResponse<AiInventoryProviderSummary>> SearchAiInventoryProviderSummariesAsync(
		Provider provider,
		string organizationName,
		[Body] AiInventoryFilter body,
		[Query] string? cursor,
		[Query] int? limit,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get the AI inventory summary for a specific provider
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{organizationName}/ai-inventory/providers/summary")]
	Task<AiInventoryProviderSummaryResponse> GetAiInventoryProviderSummaryAsync(
		Provider provider,
		string organizationName,
		[Body] GetAiInventoryProviderSummaryBody body,
		CancellationToken cancellationToken);

	/// <summary>
	/// List AI inventory marker summaries for an organization
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{organizationName}/ai-inventory/markers/summaries/search")]
	Task<ListResponse<AiInventoryMarkerSummary>> SearchAiInventoryMarkerSummariesAsync(
		Provider provider,
		string organizationName,
		[Body] AiInventoryFilter body,
		[Query] string? cursor,
		[Query] int? limit,
		CancellationToken cancellationToken);

	/// <summary>
	/// List repositories that have AI inventory items
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{organizationName}/ai-inventory/repositories/search")]
	Task<ListResponse<AiInventoryRepositoryInfo>> SearchAiInventoryRepositoriesAsync(
		Provider provider,
		string organizationName,
		[Body] AiInventoryFilter body,
		[Query] string? cursor,
		[Query] int? limit,
		CancellationToken cancellationToken);

	/// <summary>
	/// List AI inventory repository summaries for an organization
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{organizationName}/ai-inventory/repositories/summaries/search")]
	Task<ListResponse<AiInventoryRepositorySummary>> SearchAiInventoryRepositorySummariesAsync(
		Provider provider,
		string organizationName,
		[Body] AiInventoryFilter body,
		[Query] string? cursor,
		[Query] int? limit,
		CancellationToken cancellationToken);

	/// <summary>
	/// List AI inventory location summaries for an organization
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{organizationName}/ai-inventory/locations/summaries/search")]
	Task<ListResponse<AiInventoryLocationSummary>> SearchAiInventoryLocationSummariesAsync(
		Provider provider,
		string organizationName,
		[Body] AiInventoryFilter body,
		[Query] string? cursor,
		[Query] int? limit,
		CancellationToken cancellationToken);
}
