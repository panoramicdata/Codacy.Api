namespace Codacy.Api.Models;

// ===== People Models =====

/// <summary>
/// Suggested author
/// </summary>
public class SuggestedAuthor
{
	/// <summary>Author name</summary>
	public string? Name { get; set; }

	/// <summary>Author email</summary>
	public string? Email { get; set; }

	/// <summary>Commit count</summary>
	public int? CommitCount { get; set; }
}
