namespace Codacy.Api.Models;

/// <summary>
/// Commit delta issue
/// </summary>
public class CommitDeltaIssue
{
	/// <summary>Commit issue</summary>
	public required Issue CommitIssue { get; set; }

	/// <summary>Delta type (Added/Fixed)</summary>
	public required DeltaType DeltaType { get; set; }
}
