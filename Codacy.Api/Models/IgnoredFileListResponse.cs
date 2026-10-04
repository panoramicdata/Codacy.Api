namespace Codacy.Api.Models;

/// <summary>
/// Ignored file list response
/// </summary>
public class IgnoredFileListResponse
{
	/// <summary>If the repository has a configuration file that controls which files are ignored</summary>
	public required bool HasCodacyConfigurationFile { get; set; }

	/// <summary>Ignored files</summary>
	public required List<IgnoredFile> Data { get; set; }

	/// <summary>Pagination info</summary>
	public PaginationInfo? Pagination { get; set; }
}
