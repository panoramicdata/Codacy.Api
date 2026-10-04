using Codacy.Api.Models;
using Refit;

namespace Codacy.Api.Interfaces;

/// <summary>
/// Interface for organization settings, membership and audit API operations
/// </summary>
public interface IOrganizationSettingsApi
{
	/// <summary>
	/// Get organization by provider installation id
	/// </summary>
	[Get("/api/v3/organizations/{provider}/installation/{installationId}")]
	Task<OrganizationResponse> GetOrganizationByInstallationIdAsync(
		Provider provider,
		long installationId,
		CancellationToken cancellationToken);

	/// <summary>
	/// Add an organization to Codacy
	/// </summary>
	[Post("/api/v3/organizations")]
	Task<AddOrganizationResponse> AddOrganizationAsync(
		[Body] AddOrganizationBody body,
		CancellationToken cancellationToken);

	/// <summary>
	/// Apply default settings to all repositories
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{organizationName}/integrations/providerSettings/apply")]
	Task ApplyProviderSettingsAsync(
		Provider provider,
		string organizationName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get Git provider settings
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/integrations/providerSettings")]
	Task<ProviderIntegrationSettingsBody> GetProviderSettingsAsync(
		Provider provider,
		string organizationName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Create or update Git provider settings
	/// </summary>
	[Patch("/api/v3/organizations/{provider}/{organizationName}/integrations/providerSettings")]
	Task UpdateProviderSettingsAsync(
		Provider provider,
		string organizationName,
		[Body] ProviderIntegrationSettingsPatchBody body,
		CancellationToken cancellationToken);

	/// <summary>
	/// Retrieve the onboarding progress of an organization
	/// </summary>
	[Get("/api/v3/onboarding/organizations/{provider}/{organizationName}/progress")]
	Task<OrganizationOnboardingProgressResponse> RetrieveOrganizationOnboardingProgressAsync(
		Provider provider,
		string organizationName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Configure what the organization members can do across the Codacy platform
	/// </summary>
	[Patch("/api/v3/organizations/{provider}/{organizationName}/analysisConfigurationMinimumPermission")]
	Task PatchOrganizationSettingsAsync(
		Provider provider,
		string organizationName,
		[Body] MembershipPrivilegesBody body,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get the status of Codacy Git provider app permissions for an organization
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/gitProviderAppPermissions")]
	Task<GitProviderAppPermissions> GetGitProviderAppPermissionsAsync(
		Provider provider,
		string organizationName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Update the join mode of an organization
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{organizationName}/joinMode")]
	Task UpdateJoinModeAsync(
		Provider provider,
		string organizationName,
		[Body] JoinModeRequest body,
		CancellationToken cancellationToken);

	/// <summary>
	/// Check if the user can leave the organization
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/people/leave/check")]
	Task<LeaveOrgCheckResult> CheckIfUserCanLeaveAsync(
		Provider provider,
		string organizationName,
		CancellationToken cancellationToken);

	/// <summary>
	/// List requests to join an organization
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/join")]
	Task<ListResponse<RequestToJoin>> ListOrganizationJoinRequestsAsync(
		Provider provider,
		string organizationName,
		[Query] string? cursor,
		[Query] int? limit,
		[Query] string? search,
		CancellationToken cancellationToken);

	/// <summary>
	/// Decline requests to join an organization
	/// </summary>
	[Delete("/api/v3/organizations/{provider}/{organizationName}/join")]
	Task DeclineRequestsToJoinOrganizationAsync(
		Provider provider,
		string organizationName,
		[Body] List<string> emails,
		CancellationToken cancellationToken);

	/// <summary>
	/// Delete a request to join an organization
	/// </summary>
	[Delete("/api/v3/organizations/{provider}/{organizationName}/join/{accountIdentifier}")]
	Task DeleteOrganizationJoinRequestAsync(
		Provider provider,
		string organizationName,
		long accountIdentifier,
		CancellationToken cancellationToken);

	/// <summary>
	/// Retrieve the audit logs for the organization
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/audit")]
	Task<List<AuditLog>> ListAuditLogsForOrganizationAsync(
		Provider provider,
		string organizationName,
		[Query][AliasAs("from")] long? fromTimestamp,
		[Query][AliasAs("to")] long? toTimestamp,
		CancellationToken cancellationToken);

	/// <summary>
	/// Check if the submodules option is enabled for the organization
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/settings/submodules/check")]
	Task<CheckSubmodulesResponse> CheckSubmodulesAsync(
		Provider provider,
		string organizationName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get AI Risk Checklist for an organization
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/ai-risk-checklist")]
	Task<AiRiskChecklistResponse> GetAiRiskCheckListAsync(
		Provider provider,
		string organizationName,
		CancellationToken cancellationToken);
}
