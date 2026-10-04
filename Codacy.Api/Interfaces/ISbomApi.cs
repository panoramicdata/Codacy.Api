using Codacy.Api.Models;
using Refit;

namespace Codacy.Api.Interfaces;

/// <summary>
/// Interface for SBOM (software bill of materials) API operations
/// </summary>
public interface ISbomApi
{
	/// <summary>
	/// Search the SBOM dependencies used across the organization
	/// </summary>
	/// <remarks>
	/// The body is non-nullable because Codacy rejects a null one. Pass an empty instance to search
	/// unfiltered. <c>sortColumn</c> is <c>severity</c> (default) or <c>ossfScore</c>; <c>columnOrder</c> is
	/// <c>asc</c> or <c>desc</c> (default).
	/// </remarks>
	[Post("/api/v3/organizations/{provider}/{organizationName}/sbom/dependencies/search")]
	Task<SearchSbomDependenciesResponse> SearchSbomDependenciesAsync(
		Provider provider,
		string organizationName,
		[Body] SearchSbomDependenciesBody body,
		[Query] string? cursor,
		[Query] int? limit,
		[Query] string? sortColumn,
		[Query] string? columnOrder,
		CancellationToken cancellationToken);

	/// <summary>
	/// Search the repositories where a version of an SBOM dependency is used
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{organizationName}/sbom/dependencies/repositories/search")]
	Task<SearchRepositoriesOfSbomDependencyResponse> SearchRepositoriesOfSbomDependencyAsync(
		Provider provider,
		string organizationName,
		[Body] SearchRepositoriesOfSbomDependencyBody body,
		[Query] string? cursor,
		[Query] int? limit,
		CancellationToken cancellationToken);

	/// <summary>
	/// List repositories with SBOM dependency information
	/// </summary>
	/// <remarks>
	/// The body is non-nullable because Codacy rejects a null one. Pass an empty instance to search unfiltered.
	/// </remarks>
	[Post("/api/v3/organizations/{provider}/{organizationName}/sbom/repositories/search")]
	Task<SearchSbomRepositoriesResponse> SearchSbomRepositoriesAsync(
		Provider provider,
		string organizationName,
		[Body] SearchSbomRepositoriesBody body,
		[Query] string? cursor,
		[Query] int? limit,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get a presigned URL for the latest SBOM of a repository
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/projects/{repositoryName}/sbom")]
	Task<SbomPresignResponse> GetRepositorySbomPresignedUrlAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		CancellationToken cancellationToken);
}
