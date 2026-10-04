namespace Codacy.Api.Models;

/// <summary>
/// Commit with the branches that contain it
/// </summary>
public class CommitWithBranches : Commit
{
	/// <summary>List of branches containing the commit</summary>
	public List<Branch>? Branches { get; set; }
}
