using Codacy.Api.Models;
using Refit;

namespace Codacy.Api.Interfaces;

/// <summary>
/// Interface for Repositories API operations
/// </summary>
public interface IRepositoriesApi
{
	/// <summary>
	/// Get a repository
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/repositories/{repositoryName}")]
	Task<RepositoryResponse> GetRepositoryAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Delete a repository
	/// </summary>
	[Delete("/api/v3/organizations/{provider}/{organizationName}/repositories/{repositoryName}")]
	Task DeleteRepositoryAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Follow a repository
	/// </summary>
	[Post("/api/v3/organizations/{provider}/{organizationName}/repositories/{repositoryName}/follow")]
	Task<AddedStateResponse> FollowRepositoryAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Unfollow a repository
	/// </summary>
	[Delete("/api/v3/organizations/{provider}/{organizationName}/repositories/{repositoryName}/follow")]
	Task UnfollowRepositoryAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		CancellationToken cancellationToken);

	/// <summary>
	/// List repository branches
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/repositories/{repositoryName}/branches")]
	Task<BranchListResponse> ListRepositoryBranchesAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		[Query] bool? enabled,
		[Query] string? cursor,
		[Query] int? limit,
		[Query] string? search,
		[Query] string? sort,
		[Query] string? direction,
		CancellationToken cancellationToken);

	/// <summary>
	/// Update repository branch configuration
	/// </summary>
	[Patch("/api/v3/organizations/{provider}/{organizationName}/repositories/{repositoryName}/branches/{branchName}")]
	Task UpdateRepositoryBranchConfigurationAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		string branchName,
		[Body] UpdateRepositoryBranchConfigurationBody body,
		CancellationToken cancellationToken);

	/// <summary>
	/// Set branch as default
	/// </summary>
	/// <remarks>
	/// There is no set-default route. The API models the default branch as a property of the branch,
	/// so this delegates to <see cref="UpdateRepositoryBranchConfigurationAsync"/>. Declared as a
	/// default implementation rather than an operation, so Refit leaves it alone.
	/// </remarks>
	Task SetRepositoryBranchAsDefaultAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		string branchName,
		CancellationToken cancellationToken)
		=> UpdateRepositoryBranchConfigurationAsync(
			provider,
			organizationName,
			repositoryName,
			branchName,
			new UpdateRepositoryBranchConfigurationBody { IsDefault = true },
			cancellationToken);

	/// <summary>
	/// Get commit quality settings
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/repositories/{repositoryName}/settings/quality/commits")]
	Task<QualitySettingsResponse> GetCommitQualitySettingsAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Update commit quality settings
	/// </summary>
	[Put("/api/v3/organizations/{provider}/{organizationName}/repositories/{repositoryName}/settings/quality/commits")]
	Task<QualitySettingsResponse> UpdateCommitQualitySettingsAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		[Body] QualityGate settings,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get pull request quality settings
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/repositories/{repositoryName}/settings/quality/pull-requests")]
	Task<QualitySettingsResponse> GetPullRequestQualitySettingsAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Update pull request quality settings
	/// </summary>
	[Put("/api/v3/organizations/{provider}/{organizationName}/repositories/{repositoryName}/settings/quality/pull-requests")]
	Task<QualitySettingsResponse> UpdatePullRequestQualitySettingsAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		[Body] QualityGate settings,
		CancellationToken cancellationToken);

	/// <summary>
	/// List repository files
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/repositories/{repositoryName}/files")]
	Task<FileListResponse> ListFilesAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		[Query] string? branch,
		[Query] string? search,
		[Query] string? sort,
		[Query] string? direction,
		[Query] string? cursor,
		[Query] int? limit,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get file with analysis
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/repositories/{repositoryName}/files/{fileId}")]
	Task<FileInformationWithAnalysis> GetFileWithAnalysisAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		long fileId,
		CancellationToken cancellationToken);

	/// <summary>
	/// Add a repository
	/// </summary>
	[Post("/api/v3/repositories")]
	Task<Repository> AddRepositoryAsync(
		[Body] AddRepositoryBody body,
		CancellationToken cancellationToken);
}
