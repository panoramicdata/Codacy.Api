using Codacy.Api.Models;
using Refit;

namespace Codacy.Api.Interfaces;

/// <summary>
/// Interface for Account/User API operations
/// </summary>
public interface IAccountApi
{
	/// <summary>
	/// Get the authenticated user
	/// </summary>
	[Get("/api/v3/user")]
	Task<UserResponse> GetUserAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Delete the authenticated user
	/// </summary>
	[Delete("/api/v3/user")]
	Task DeleteUserAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Update the authenticated user
	/// </summary>
	[Patch("/api/v3/user")]
	Task<UserResponse> UpdateUserAsync([Body] UserBody body, CancellationToken cancellationToken);

	/// <summary>
	/// List user organizations
	/// </summary>
	[Get("/api/v3/user/organizations")]
	Task<ListResponse<Organization>> ListUserOrganizationsAsync(
		[Query] string? cursor,
		[Query] int? limit,
		CancellationToken cancellationToken);

	/// <summary>
	/// List organizations for a provider
	/// </summary>
	[Get("/api/v3/user/organizations/{provider}")]
	Task<ListResponse<Organization>> ListOrganizationsAsync(
		Provider provider,
		[Query] string? cursor,
		[Query] int? limit,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get organization for the authenticated user
	/// </summary>
	[Get("/api/v3/user/organizations/{provider}/{remoteOrganizationName}")]
	Task<OrganizationResponse> GetUserOrganizationAsync(
		Provider provider,
		string remoteOrganizationName,
		CancellationToken cancellationToken);

	/// <summary>
	/// List user emails
	/// </summary>
	[Get("/api/v3/user/emails")]
	Task<UserEmailsResponse> ListUserEmailsAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Remove an email from user account
	/// </summary>
	[Post("/api/v3/user/emails/remove")]
	Task RemoveUserEmailAsync([Body] string email, CancellationToken cancellationToken);

	/// <summary>
	/// Get email notification settings
	/// </summary>
	[Get("/api/v3/user/emails/settings")]
	Task<EmailNotificationSettingsResponse> GetEmailSettingsAsync(CancellationToken cancellationToken);

	/// <summary>
	/// Update email notification settings
	/// </summary>
	[Patch("/api/v3/user/emails/settings")]
	Task UpdateEmailSettingsAsync(
		[Body] EmailNotificationSettingsOptional settings,
		CancellationToken cancellationToken);

	/// <summary>
	/// Set an email as default
	/// </summary>
	[Post("/api/v3/user/emails/set-default")]
	Task SetDefaultEmailAsync(
		[Body] string email,
		CancellationToken cancellationToken);

	/// <summary>
	/// List user integrations
	/// </summary>
	[Get("/api/v3/user/integrations")]
	Task<ListResponse<Integration>> ListUserIntegrationsAsync(
		[Query] string? cursor,
		[Query] int? limit,
		CancellationToken cancellationToken);

	/// <summary>
	/// Delete an integration for the authenticated user
	/// </summary>
	[Delete("/api/v3/user/integrations/{provider}")]
	Task DeleteIntegrationAsync(
		Provider provider,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get user API tokens
	/// </summary>
	[Get("/api/v3/user/tokens")]
	Task<ListResponse<ApiToken>> GetUserApiTokensAsync(
		[Query] string? cursor,
		[Query] int? limit,
		CancellationToken cancellationToken);

	/// <summary>
	/// Create user API token
	/// </summary>
	[Post("/api/v3/user/tokens")]
	Task<ApiToken> CreateUserApiTokenAsync(
		[Body] ApiTokenCreateRequest? request,
		CancellationToken cancellationToken);

	/// <summary>
	/// Delete user API token
	/// </summary>
	[Delete("/api/v3/user/tokens/{tokenId}")]
	Task DeleteUserApiTokenAsync(
		long tokenId,
		CancellationToken cancellationToken);
}
