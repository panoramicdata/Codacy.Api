namespace Codacy.Api.Models;

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
