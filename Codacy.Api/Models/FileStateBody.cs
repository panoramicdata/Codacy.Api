namespace Codacy.Api.Models;

/// <summary>
/// Ignored status of a file
/// </summary>
public class FileStateBody
{
	/// <summary>Relative path of the file in the repository</summary>
	public required string Filepath { get; set; }

	/// <summary>True if the file is ignored</summary>
	public required bool Ignored { get; set; }
}
