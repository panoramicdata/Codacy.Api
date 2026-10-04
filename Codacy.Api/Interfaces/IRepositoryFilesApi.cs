using Codacy.Api.Models;
using Refit;

namespace Codacy.Api.Interfaces;

/// <summary>
/// Interface for repository file and directory API operations
/// </summary>
public interface IRepositoryFilesApi
{
	/// <summary>
	/// List the folders of a repository
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/repositories/{repositoryName}/directories")]
	Task<ListResponse<DirectoryWithAnalysisInfo>> ListDirectoriesAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		[Query] string? branch,
		[Query] string? path,
		[Query] string? sort,
		[Query] string? direction,
		[Query] string? cursor,
		[Query] int? limit,
		CancellationToken cancellationToken);

	/// <summary>
	/// List the ignored files of a repository
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/repositories/{repositoryName}/ignored-files")]
	Task<IgnoredFileListResponse> ListIgnoredFilesAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		[Query] string? branch,
		[Query] string? search,
		[Query] string? cursor,
		[Query] int? limit,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get the duplicated code blocks of a file
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/repositories/{repositoryName}/files/{fileId}/duplication")]
	Task<ListResponse<FileClone>> GetFileClonesAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		long fileId,
		[Query] string? cursor,
		[Query] int? limit,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get the issues of a file
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/repositories/{repositoryName}/files/{fileId}/issues")]
	Task<ListResponse<CommitIssue>> GetFileIssuesAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		long fileId,
		[Query] string? cursor,
		[Query] int? limit,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get the content of a file, optionally restricted to a range of lines
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/repositories/{repositoryName}/files/{filePath}/content")]
	Task<CodeBlockLineListResponse> GetFileContentAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		string filePath,
		[Query] int? startLine,
		[Query] int? endLine,
		[Query] string? commitRef,
		CancellationToken cancellationToken);

	/// <summary>
	/// Get the coverage of a file
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/repositories/{repositoryName}/files/{fileId}/coverage")]
	Task<GetFileCoverageResponse> GetFileCoverageAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		long fileId,
		CancellationToken cancellationToken);

	/// <summary>
	/// Update the ignored status of a file
	/// </summary>
	[Patch("/api/v3/organizations/{provider}/{organizationName}/repositories/{repositoryName}/file")]
	Task UpdateFileStateAsync(
		Provider provider,
		string organizationName,
		string repositoryName,
		[Body] FileStateBody body,
		CancellationToken cancellationToken);
}
