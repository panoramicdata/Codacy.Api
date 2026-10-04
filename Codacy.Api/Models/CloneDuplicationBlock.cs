namespace Codacy.Api.Models;

/// <summary>
/// Occurrence of a duplicated code block
/// </summary>
public class CloneDuplicationBlock
{
	/// <summary>Path of the file</summary>
	public required string Path { get; set; }

	/// <summary>File ID</summary>
	public required long FileId { get; set; }

	/// <summary>File data ID</summary>
	public required long FileDataId { get; set; }

	/// <summary>First line of the block</summary>
	public required long FromLine { get; set; }

	/// <summary>Last line of the block</summary>
	public required long ToLine { get; set; }
}
