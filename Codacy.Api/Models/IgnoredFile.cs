namespace Codacy.Api.Models;

/// <summary>
/// Ignored file
/// </summary>
public class IgnoredFile
{
	/// <summary>Relative path of the file in the repository</summary>
	public required string Filepath { get; set; }
}
