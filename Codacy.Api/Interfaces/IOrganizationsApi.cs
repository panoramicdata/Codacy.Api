using Codacy.Api.Models;
using Refit;

namespace Codacy.Api.Interfaces;

/// <summary>
/// Interface for Organizations API operations
/// </summary>
public interface IOrganizationsApi
{
	/// <summary>
	/// Get an organization
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}")]
	Task<OrganizationResponse> GetOrganizationAsync(
		Provider provider,
		string organizationName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Delete an organization
	/// </summary>
	[Delete("/api/v3/organizations/{provider}/{organizationName}")]
	Task DeleteOrganizationAsync(
		Provider provider,
		string organizationName,
		CancellationToken cancellationToken);

	/// <summary>
	/// List organization repositories
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/repositories")]
	Task<ListResponse<Repository>> ListOrganizationRepositoriesAsync(
		Provider provider,
		string organizationName,
		[Query] string? cursor,
		[Query] int? limit,
		[Query] string? search,
		[Query] string? filter,
		[Query] string? languages,
		[Query] string? segments,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get organization billing information
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/billing")]
	Task<OrganizationBillingInformationResponse> GetOrganizationBillingAsync(
		Provider provider,
		string organizationName,
		CancellationToken cancellationToken);

	/// <summary>
	/// List people from an organization
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/people")]
	Task<ListResponse<OrganizationPerson>> ListPeopleFromOrganizationAsync(
		Provider provider,
		string organizationName,
		[Query] string? cursor,
		[Query] int? limit,
		[Query] string? search,
		[Query] bool onlyMembers,
		CancellationToken cancellationToken);

	/// <summary>
	/// Add people to an organization
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{organizationName}/people")]
	Task AddPeopleToOrganizationAsync(
		Provider provider,
		string organizationName,
		[Body] List<string> emails,
		CancellationToken cancellationToken);

	/// <summary>
	/// Remove people from an organization
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{organizationName}/people/remove")]
	Task<OrganizationRemovePeopleResponse> RemovePeopleFromOrganizationAsync(
		Provider provider,
		string organizationName,
		[Body] OrganizationRemovePeopleBody body,
		CancellationToken cancellationToken);

	/// <summary>
	/// Clean organization cache
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{organizationName}/cache/clean")]
	Task CleanCacheAsync(
		Provider provider,
		string organizationName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Join an organization
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{organizationName}/join")]
	Task<JoinResponse> JoinOrganizationAsync(
		Provider provider,
		string organizationName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Sync organization name with Git provider
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{organizationName}/sync")]
	Task<SyncProviderSettingOrganizationResponse> SyncOrganizationNameAsync(
		Provider provider,
		string organizationName,
		CancellationToken cancellationToken);
}
