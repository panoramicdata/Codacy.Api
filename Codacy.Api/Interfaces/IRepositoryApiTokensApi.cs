using Codacy.Api.Models;
using Refit;

namespace Codacy.Api.Interfaces;

/// <summary>
/// Interface for repository API token operations
/// </summary>
public interface IRepositoryApiTokensApi
{
	/// <summary>
	/// List repository API tokens
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/repositories/{repositoryName}/tokens")]
	Task<ListResponse<ApiToken>> ListRepositoryApiTokensAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Create a repository API token
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{organizationName}/repositories/{repositoryName}/tokens")]
	Task<ApiTokenResponse> CreateRepositoryApiTokenAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		[Body] RepositoryApiTokenCreateRequest? body,
		CancellationToken cancellationToken);

	/// <summary>
	/// Delete several repository API tokens
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{organizationName}/repositories/{repositoryName}/tokens/delete")]
	Task DeleteRepositoryApiTokensAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		[Body] RepositoryApiTokensDeleteRequest body,
		CancellationToken cancellationToken);

	/// <summary>
	/// Delete a repository API token
	/// </summary>
	[Delete("/api/v3/organizations/{provider}/{organizationName}/repositories/{repositoryName}/tokens/{tokenId}")]
	Task DeleteRepositoryApiTokenAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		long tokenId,
		CancellationToken cancellationToken);
}
