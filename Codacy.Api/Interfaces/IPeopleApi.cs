using Codacy.Api.Models;
using Refit;

namespace Codacy.Api.Interfaces;

/// <summary>
/// Interface for People API operations
/// </summary>
public interface IPeopleApi
{
	/// <summary>
	/// List people in an organization
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{remoteOrganizationName}/people")]
	Task<ListResponse<OrganizationPerson>> ListPeopleFromOrganizationAsync(
		Provider provider,
		string remoteOrganizationName,
		[Query] string? cursor,
		[Query] int? limit,
		[Query] string? search,
		[Query] bool? onlyMembers,
		CancellationToken cancellationToken);

	/// <summary>
	/// Add people to organization
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{remoteOrganizationName}/people")]
	Task AddPeopleToOrganizationAsync(
		Provider provider,
		string remoteOrganizationName,
		[Body] List<string> emails,
		CancellationToken cancellationToken);

	/// <summary>
	/// Remove people from organization
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{remoteOrganizationName}/people/remove")]
	Task<OrganizationRemovePeopleResponse> RemovePeopleFromOrganizationAsync(
		Provider provider,
		string remoteOrganizationName,
		[Body] OrganizationRemovePeopleBody body,
		CancellationToken cancellationToken);

	/// <summary>
	/// List people suggestions for an organization
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{remoteOrganizationName}/people/suggestions")]
	Task<ListResponse<SuggestedAuthor>> PeopleSuggestionsForOrganizationAsync(
		Provider provider,
		string remoteOrganizationName,
		[Query] string? cursor,
		[Query] int? limit,
		[Query] string? search,
		CancellationToken cancellationToken);

	/// <summary>
	/// List people suggestions for a repository
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{remoteOrganizationName}/repositories/{repositoryName}/people/suggestions")]
	Task<ListResponse<SuggestedAuthor>> PeopleSuggestionsForRepositoryAsync(
		Provider provider,
		string remoteOrganizationName,
		string repositoryName,
		[Query] string? cursor,
		[Query] int? limit,
		[Query] string? search,
		CancellationToken cancellationToken);

	/// <summary>
	/// Export people list as CSV
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{remoteOrganizationName}/peopleCsv")]
	Task<string> ListPeopleFromOrganizationCsvAsync(
		Provider provider,
		string remoteOrganizationName,
		CancellationToken cancellationToken);
}
