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
	Task<RepositoryListResponse> ListOrganizationRepositoriesAsync(
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
	Task<ListPeopleResponse> ListPeopleFromOrganizationAsync(
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
	Task<RemovePeopleResponse> RemovePeopleFromOrganizationAsync(
		Provider provider,
		string organizationName,
		[Body] RemovePeopleBody body,
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

/// <summary>
/// Organization billing information response
/// </summary>
public class OrganizationBillingInformationResponse
{
	/// <summary>Billing data</summary>
	public required OrganizationBillingInformation Data { get; set; }
}

/// <summary>
/// Organization billing information
/// </summary>
public class OrganizationBillingInformation
{
	/// <summary>Number of seats</summary>
	public required int NumberOfSeats { get; set; }

	/// <summary>Number of purchased seats</summary>
	public required int NumberOfPurchasedSeats { get; set; }

	/// <summary>Price in cents</summary>
	public required int PriceInCents { get; set; }

	/// <summary>Price per seat in cents</summary>
	public int? PricePerSeatInCents { get; set; }

	/// <summary>Next payment date</summary>
	public DateTimeOffset? NextPaymentDate { get; set; }
}

/// <summary>
/// List people response
/// </summary>
public class ListPeopleResponse
{
	/// <summary>Pagination info</summary>
	public PaginationInfo? Pagination { get; set; }

	/// <summary>People</summary>
	public required List<Person> Data { get; set; }
}

/// <summary>
/// Person information
/// </summary>
public class Person
{
	/// <summary>Name</summary>
	public string? Name { get; set; }

	/// <summary>Email</summary>
	public required string Email { get; set; }

	/// <summary>Emails</summary>
	public required List<string> Emails { get; set; }

	/// <summary>User ID</summary>
	public long? UserId { get; set; }

	/// <summary>Committer ID</summary>
	public long? CommitterId { get; set; }

	/// <summary>Last login</summary>
	public DateTimeOffset? LastLogin { get; set; }

	/// <summary>Last analysis</summary>
	public DateTimeOffset? LastAnalysis { get; set; }

	/// <summary>Is active</summary>
	public bool? IsActive { get; set; }

	/// <summary>Can be removed</summary>
	public required bool CanBeRemoved { get; set; }
}

/// <summary>
/// Remove people body
/// </summary>
public class RemovePeopleBody
{
	/// <summary>Emails to remove</summary>
	public required List<string> Emails { get; set; }
}

/// <summary>
/// Remove people response
/// </summary>
public class RemovePeopleResponse
{
	/// <summary>Successfully removed</summary>
	public List<RemovePeopleEmailStatus>? Success { get; set; }

	/// <summary>Failed to remove</summary>
	public List<RemovePeopleEmailStatus>? Failed { get; set; }
}

/// <summary>
/// Remove people email status
/// </summary>
public class RemovePeopleEmailStatus
{
	/// <summary>Email</summary>
	public required string Email { get; set; }

	/// <summary>Error message</summary>
	public string? Error { get; set; }
}

/// <summary>
/// Join response
/// </summary>
public class JoinResponse
{
	/// <summary>Organization identifier</summary>
	public required long OrganizationIdentifier { get; set; }

	/// <summary>Join status</summary>
	public required JoinStatus JoinStatus { get; set; }
}

/// <summary>
/// Sync provider setting organization response
/// </summary>
public class SyncProviderSettingOrganizationResponse
{
	/// <summary>Synced name</summary>
	public required string Name { get; set; }
}
