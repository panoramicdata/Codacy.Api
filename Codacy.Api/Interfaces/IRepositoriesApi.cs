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

/// <summary>
/// Repository response
/// </summary>
public class RepositoryResponse
{
	/// <summary>Repository data</summary>
	public required Repository Data { get; set; }
}

/// <summary>
/// Added state response
/// </summary>
public class AddedStateResponse
{
	/// <summary>Added state</summary>
	public required AddedState Data { get; set; }
}

/// <summary>
/// Branch list response
/// </summary>
public class BranchListResponse
{
	/// <summary>Pagination info</summary>
	public PaginationInfo? Pagination { get; set; }

	/// <summary>Branches</summary>
	public required List<Branch> Data { get; set; }
}

/// <summary>
/// Update repository branch configuration body
/// </summary>
public class UpdateRepositoryBranchConfigurationBody
{
	/// <summary>Is enabled for analysis</summary>
	public bool? IsEnabled { get; set; }
}

/// <summary>
/// Repository quality settings
/// </summary>
public class RepositoryQualitySettings
{
	/// <summary>Max issue percentage</summary>
	public int? MaxIssuePercentage { get; set; }

	/// <summary>Max duplicated files percentage</summary>
	public int? MaxDuplicatedFilesPercentage { get; set; }

	/// <summary>Min coverage percentage</summary>
	public int? MinCoveragePercentage { get; set; }

	/// <summary>Max complex files percentage</summary>
	public int? MaxComplexFilesPercentage { get; set; }

	/// <summary>File duplication block threshold</summary>
	public int? FileDuplicationBlockThreshold { get; set; }

	/// <summary>File complexity value threshold</summary>
	public int? FileComplexityValueThreshold { get; set; }
}

/// <summary>
/// Repository quality settings response
/// </summary>
public class RepositoryQualitySettingsResponse
{
	/// <summary>Settings data</summary>
	public required RepositoryQualitySettings Data { get; set; }
}

/// <summary>
/// Quality gate settings
/// </summary>
public class QualityGate
{
	/// <summary>Issue threshold</summary>
	public IssueThreshold? IssueThreshold { get; set; }

	/// <summary>Security issue threshold</summary>
	public int? SecurityIssueThreshold { get; set; }

	/// <summary>Security issue minimum severity</summary>
	public SeverityLevel? SecurityIssueMinimumSeverity { get; set; }

	/// <summary>Duplication threshold</summary>
	public int? DuplicationThreshold { get; set; }

	/// <summary>Coverage threshold with decimals</summary>
	public double? CoverageThresholdWithDecimals { get; set; }

	/// <summary>Diff coverage threshold</summary>
	public int? DiffCoverageThreshold { get; set; }

	/// <summary>Complexity threshold</summary>
	public int? ComplexityThreshold { get; set; }
}

/// <summary>
/// Issue threshold
/// </summary>
public class IssueThreshold
{
	/// <summary>Threshold value</summary>
	public required int Threshold { get; set; }

	/// <summary>Minimum severity</summary>
	public SeverityLevel? MinimumSeverity { get; set; }
}

/// <summary>
/// Quality settings response
/// </summary>
public class QualitySettingsResponse
{
	/// <summary>Settings data</summary>
	public required QualitySettingsWithGatePolicy Data { get; set; }
}

/// <summary>
/// Quality settings with gate policy
/// </summary>
public class QualitySettingsWithGatePolicy
{
	/// <summary>Quality gate</summary>
	public required QualityGate QualityGate { get; set; }

	/// <summary>Repository gate policy info</summary>
	public RepositoryGatePolicy? RepositoryGatePolicyInfo { get; set; }
}

/// <summary>
/// Repository gate policy
/// </summary>
public class RepositoryGatePolicy
{
	/// <summary>Gate policy ID</summary>
	public required long Id { get; set; }

	/// <summary>Gate policy name</summary>
	public required string Name { get; set; }
}

/// <summary>
/// File list response
/// </summary>
public class FileListResponse
{
	/// <summary>Pagination info</summary>
	public PaginationInfo? Pagination { get; set; }

	/// <summary>Files</summary>
	public required List<FileWithAnalysisInfo> Data { get; set; }
}

/// <summary>
/// File with analysis info
/// </summary>
public class FileWithAnalysisInfo
{
	/// <summary>File ID</summary>
	public required long FileId { get; set; }

	/// <summary>Branch ID</summary>
	public required long BranchId { get; set; }

	/// <summary>File path</summary>
	public required string Path { get; set; }

	/// <summary>Total issues</summary>
	public required int TotalIssues { get; set; }

	/// <summary>Number of methods</summary>
	public required int NumberOfMethods { get; set; }

	/// <summary>Grade (0-100)</summary>
	public required int Grade { get; set; }

	/// <summary>Grade letter (A-F)</summary>
	public required string GradeLetter { get; set; }

	/// <summary>Complexity</summary>
	public int? Complexity { get; set; }

	/// <summary>Coverage with decimals</summary>
	public double? CoverageWithDecimals { get; set; }

	/// <summary>Lines of code</summary>
	public int? LinesOfCode { get; set; }
}

/// <summary>
/// File information with analysis
/// </summary>
public class FileInformationWithAnalysis
{
	/// <summary>File metadata</summary>
	public required FileMetadata File { get; set; }

	/// <summary>File metrics</summary>
	public FileMetrics? Metrics { get; set; }

	/// <summary>Coverage analysis</summary>
	public FileCoverageAnalysis? Coverage { get; set; }

	/// <summary>Quality info</summary>
	public FileQualityInfo? Quality { get; set; }
}

/// <summary>
/// File metadata
/// </summary>
public class FileMetadata
{
	/// <summary>Branch ID</summary>
	public long? BranchId { get; set; }

	/// <summary>Commit ID</summary>
	public required long CommitId { get; set; }

	/// <summary>Commit SHA</summary>
	public required string CommitSha { get; set; }

	/// <summary>File ID</summary>
	public required long FileId { get; set; }

	/// <summary>File data ID</summary>
	public required long FileDataId { get; set; }

	/// <summary>File path</summary>
	public required string Path { get; set; }

	/// <summary>Language</summary>
	public required string Language { get; set; }

	/// <summary>Git provider URL</summary>
	public required string GitProviderUrl { get; set; }

	/// <summary>Is ignored</summary>
	public required bool Ignored { get; set; }
}

/// <summary>
/// File metrics
/// </summary>
public class FileMetrics
{
	/// <summary>Lines of code</summary>
	public int? LinesOfCode { get; set; }

	/// <summary>Commented lines of code</summary>
	public long? CommentedLinesOfCode { get; set; }

	/// <summary>Number of methods</summary>
	public int? NumberOfMethods { get; set; }

	/// <summary>Number of classes</summary>
	public int? NumberOfClasses { get; set; }
}

/// <summary>
/// File coverage analysis
/// </summary>
public class FileCoverageAnalysis
{
	/// <summary>Coverage percentage</summary>
	public required double Coverage { get; set; }

	/// <summary>Coverable lines</summary>
	public required long CoverableLines { get; set; }

	/// <summary>Covered lines</summary>
	public required long CoveredLines { get; set; }
}

/// <summary>
/// File quality info
/// </summary>
public class FileQualityInfo
{
	/// <summary>Total issues</summary>
	public required int TotalIssues { get; set; }

	/// <summary>Grade (0-100)</summary>
	public required int Grade { get; set; }

	/// <summary>Grade letter (A-F)</summary>
	public required string GradeLetter { get; set; }

	/// <summary>Complexity</summary>
	public int? Complexity { get; set; }

	/// <summary>Duplication</summary>
	public int? Duplication { get; set; }

	/// <summary>Duplicated lines of code</summary>
	public int? DuplicatedLinesOfCode { get; set; }
}

/// <summary>
/// Add repository body
/// </summary>
public class AddRepositoryBody
{
	/// <summary>Repository full path</summary>
	public required string RepositoryFullPath { get; set; }

	/// <summary>Provider</summary>
	public required Provider Provider { get; set; }
}
