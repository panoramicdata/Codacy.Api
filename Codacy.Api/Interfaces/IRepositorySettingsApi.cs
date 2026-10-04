using Codacy.Api.Models;
using Refit;

namespace Codacy.Api.Interfaces;

/// <summary>
/// Interface for repository settings API operations
/// </summary>
public interface IRepositorySettingsApi
{
	/// <summary>
	/// Get quality settings for the specific repository
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/repositories/{repositoryName}/settings/quality/repository")]
	Task<RepositoryQualitySettingsResponse> GetQualitySettingsForRepositoryAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Update quality goals settings for the specific repository
	/// </summary>
	[Put("/api/v3/organizations/{provider}/{organizationName}/repositories/{repositoryName}/settings/quality/repository")]
	Task<RepositoryQualitySettingsResponse> UpdateRepositoryQualitySettingsAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		[Body] RepositoryQualitySettings settings,
		CancellationToken cancellationToken);

	/// <summary>
	/// Regenerate the user SSH key that Codacy uses to clone the repository
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{organizationName}/repositories/{repositoryName}/settings/ssh-user-key")]
	Task<SshKeySettingResponse> RegenerateUserSshKeyAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Regenerate the SSH key that Codacy uses to clone the repository
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{organizationName}/repositories/{repositoryName}/settings/ssh-repository-key")]
	Task<SshKeySettingResponse> RegenerateRepositorySshKeyAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get the public SSH key for the repository
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/repositories/{repositoryName}/settings/stored-ssh-key")]
	Task<SshKeySettingResponse> GetRepositoryPublicSshKeyAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Synchronize repository name and visibility with Git provider
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{organizationName}/repositories/{repositoryName}/settings/sync")]
	Task<SyncProviderSettingResponse> SyncRepositoryWithProviderAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get the status of the repository setting Run analysis on your build server
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/repositories/{repositoryName}/settings/analysis")]
	Task<BuildServerAnalysisSettingResponse> GetBuildServerAnalysisSettingAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Update the status of the repository setting Run analysis on your build server
	/// </summary>
	[Patch("/api/v3/organizations/{provider}/{organizationName}/repositories/{repositoryName}/settings/analysis")]
	Task<BuildServerAnalysisSettingResponse> UpdateBuildServerAnalysisSettingAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		[Body] BuildServerAnalysisSettingRequest body,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get the list of all languages with their extensions and enabled status
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/repositories/{repositoryName}/settings/languages")]
	Task<RepositoryLanguageResponse> GetRepositoryLanguagesAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Configure language settings for this repository
	/// </summary>
	[Patch("/api/v3/organizations/{provider}/{organizationName}/repositories/{repositoryName}/settings/languages")]
	Task PatchRepositoryLanguageResponseSettingsAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		[Body] RepositoryLanguagesBody body,
		CancellationToken cancellationToken);

	/// <summary>
	/// Reset quality settings for the commits of a repository to default values
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{organizationName}/repositories/{repositoryName}/settings/quality/commits/reset")]
	Task<QualitySettingsResponse> ResetCommitsQualitySettingsAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Reset quality settings for the pull requests of a repository to default values
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{organizationName}/repositories/{repositoryName}/settings/quality/pull-requests/reset")]
	Task<QualitySettingsResponse> ResetPullRequestsQualitySettingsAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Reset quality settings for the repository to default values
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{organizationName}/repositories/{repositoryName}/settings/quality/repository/reset")]
	Task<RepositoryQualitySettingsResponse> ResetRepositoryQualitySettingsAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get the Git provider integration settings of the repository
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/repositories/{repositoryName}/integrations/providerSettings")]
	Task<RepositoryIntegrationSettings> GetRepositoryIntegrationsSettingsAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Update the Git provider integration settings of the repository
	/// </summary>
	[Patch("/api/v3/organizations/{provider}/{organizationName}/repositories/{repositoryName}/integrations/providerSettings")]
	Task UpdateRepositoryIntegrationsSettingsAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		[Body] ProviderIntegrationSettingsPatchBody body,
		CancellationToken cancellationToken);

	/// <summary>
	/// Create the post-commit hook on the Git provider
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/repositories/{repositoryName}/integrations/postCommitHook")]
	Task CreatePostCommitHookAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Refresh the repository Git provider integration
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{organizationName}/repositories/{repositoryName}/integrations/refreshProvider")]
	Task RefreshProviderRepositoryIntegrationAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Create a pull request that adds the Codacy badge to the repository (GitHub only)
	/// </summary>
	[Post("/api/v3/organizations/gh/{organizationName}/repositories/{repositoryName}/badge")]
	Task CreateBadgePullRequestAsync(
		string organizationName,
		string repositoryName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get the Codacy checks required before merge on a branch
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/repositories/{repositoryName}/branches/{branchName}/required-checks")]
	Task<BranchRequiredChecksResponse> GetBranchRequiredChecksAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		string branchName,
		CancellationToken cancellationToken);
}
