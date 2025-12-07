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
	[Get("/api/v3/repositories/{provider}/{organizationName}/{repositoryName}")]
	Task<RepositoryResponse> GetRepositoryAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Delete a repository
	/// </summary>
	[Delete("/api/v3/repositories/{provider}/{organizationName}/{repositoryName}")]
	Task DeleteRepositoryAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Follow a repository
	/// </summary>
	[Post("/api/v3/repositories/{provider}/{organizationName}/{repositoryName}/follow")]
	Task<AddedStateResponse> FollowRepositoryAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Unfollow a repository
	/// </summary>
	[Post("/api/v3/repositories/{provider}/{organizationName}/{repositoryName}/unfollow")]
	Task UnfollowRepositoryAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		CancellationToken cancellationToken);

	/// <summary>
	/// List repository branches
	/// </summary>
	[Get("/api/v3/repositories/{provider}/{organizationName}/{repositoryName}/branches")]
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
	[Patch("/api/v3/repositories/{provider}/{organizationName}/{repositoryName}/branches/{branchName}")]
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
	[Post("/api/v3/repositories/{provider}/{organizationName}/{repositoryName}/branches/{branchName}/set-default")]
	Task SetRepositoryBranchAsDefaultAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		string branchName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get quality settings for repository
	/// </summary>
	[Get("/api/v3/repositories/{provider}/{organizationName}/{repositoryName}/settings/quality")]
	Task<RepositoryQualitySettingsResponse> GetQualitySettingsForRepositoryAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Update quality settings for repository
	/// </summary>
	[Patch("/api/v3/repositories/{provider}/{organizationName}/{repositoryName}/settings/quality")]
	Task<RepositoryQualitySettingsResponse> UpdateRepositoryQualitySettingsAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		[Body] RepositoryQualitySettings settings,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get commit quality settings
	/// </summary>
	[Get("/api/v3/repositories/{provider}/{organizationName}/{repositoryName}/settings/quality/commits")]
	Task<QualitySettingsResponse> GetCommitQualitySettingsAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Update commit quality settings
	/// </summary>
	[Patch("/api/v3/repositories/{provider}/{organizationName}/{repositoryName}/settings/quality/commits")]
	Task<QualitySettingsResponse> UpdateCommitQualitySettingsAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		[Body] QualityGate settings,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get pull request quality settings
	/// </summary>
	[Get("/api/v3/repositories/{provider}/{organizationName}/{repositoryName}/settings/quality/pullrequests")]
	Task<QualitySettingsResponse> GetPullRequestQualitySettingsAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Update pull request quality settings
	/// </summary>
	[Patch("/api/v3/repositories/{provider}/{organizationName}/{repositoryName}/settings/quality/pullrequests")]
	Task<QualitySettingsResponse> UpdatePullRequestQualitySettingsAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		[Body] QualityGate settings,
		CancellationToken cancellationToken);

	/// <summary>
	/// List repository files
	/// </summary>
	[Get("/api/v3/repositories/{provider}/{organizationName}/{repositoryName}/files")]
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
	[Get("/api/v3/repositories/{provider}/{organizationName}/{repositoryName}/files/{fileId}")]
	Task<FileInformationWithAnalysis> GetFileWithAnalysisAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		long fileId,
		CancellationToken cancellationToken);

	/// <summary>
	/// Reanalyze a commit
	/// </summary>
	[Post("/api/v3/repositories/{provider}/{organizationName}/{repositoryName}/commits/{commitUuid}/reanalyze")]
	Task ReanalyzeCommitAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		string commitUuid,
		[Query] bool cleanCache,
		CancellationToken cancellationToken);

	/// <summary>
	/// Add a repository
	/// </summary>
	[Post("/api/v3/repositories")]
	Task<Repository> AddRepositoryAsync(
		[Body] AddRepositoryBody body,
		CancellationToken cancellationToken);
}
