namespace Codacy.Api.Models;

/// <summary>
/// A duplicated code block within a file, including its occurrences
/// </summary>
public class FileClone
{
	/// <summary>Clone ID</summary>
	public required long Id { get; set; }

	/// <summary>Occurrences of the duplicated block</summary>
	public required List<CloneDuplicationBlock> Occurrences { get; set; }
}
