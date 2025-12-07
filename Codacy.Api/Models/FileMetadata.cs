namespace Codacy.Api.Models;

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
