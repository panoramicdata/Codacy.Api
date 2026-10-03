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

	/// <summary>
	/// List the webhook endpoints of an organization. Requires organization write permission.
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/integrations/webhooks")]
	Task<WebhookEndpointList> ListWebhookEndpointsAsync(
		Provider provider,
		string organizationName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Add a webhook endpoint to an organization. Codacy POSTs a <c>quality.analysis.completed</c>
	/// delivery to it when a branch or pull request analysis finishes. The response holds the
	/// signing secret, which is never returned again. Requires organization write permission and
	/// a plan with webhooks enabled (403 otherwise).
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{organizationName}/integrations/webhooks")]
	Task<WebhookEndpointCreated> CreateWebhookEndpointAsync(
		Provider provider,
		string organizationName,
		[Body] CreateWebhookEndpointBody body,
		CancellationToken cancellationToken);

	/// <summary>
	/// Delete a webhook endpoint from an organization. Requires organization write permission.
	/// </summary>
	[Delete("/api/v3/organizations/{provider}/{organizationName}/integrations/webhooks/{webhookId}")]
	Task DeleteWebhookEndpointAsync(
		Provider provider,
		string organizationName,
		Guid webhookId,
		CancellationToken cancellationToken);
}
