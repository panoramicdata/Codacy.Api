using Codacy.Api.Models;
using Refit;

namespace Codacy.Api.Interfaces;

/// <summary>
/// Interface for Segments API operations
/// </summary>
public interface ISegmentsApi
{
	/// <summary>
	/// Get the status of the segments synchronization
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/segments/sync")]
	Task<SegmentsSyncStatusResponse> GetSegmentsSyncStatusAsync(
		Provider provider,
		string organizationName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Synchronize the segments of the organization with the Git provider
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{organizationName}/segments/sync")]
	Task SyncSegmentsAsync(
		Provider provider,
		string organizationName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get the segment keys for the organization
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/segments/keys")]
	Task<ListResponse<string>> GetSegmentsKeysAsync(
		Provider provider,
		string organizationName,
		[Query] string? cursor,
		[Query] int? limit,
		[Query] string? search,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get the segment keys with IDs for the organization
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/segments/keys/ids")]
	Task<ListResponse<SegmentKeyWithId>> GetSegmentsKeysWithIdsAsync(
		Provider provider,
		string organizationName,
		[Query] string? cursor,
		[Query] int? limit,
		[Query] string? search,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get the segment values for the organization by segment key
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/segments/{segmentKey}/values")]
	Task<ListResponse<SegmentEntry>> GetSegmentsAsync(
		Provider provider,
		string organizationName,
		string segmentKey,
		[Query] string? cursor,
		[Query] string? search,
		[Query] int? limit,
		CancellationToken cancellationToken);
}
