namespace Codacy.Api.Models;

/// <summary>
/// Human-readable Git diff of a commit or pull request
/// </summary>
public class DiffResponse
{
	/// <summary>The diff, in the output format of the git diff command</summary>
	public required string Diff { get; set; }
}
