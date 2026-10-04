namespace Codacy.Api.Models;

/// <summary>
/// Details of a commit
/// </summary>
public class CommitDetailsV2
{
	/// <summary>The commit</summary>
	public required Commit Commit { get; set; }

	/// <summary>The repository of the commit</summary>
	public required RepositoryIdentificationV2 Repository { get; set; }
}
