namespace Codacy.Api.Models;

/// <summary>
/// Identifies a commit to reanalyze
/// </summary>
public class CommitUuidRequest
{
	/// <summary>UUID or SHA string that identifies the commit</summary>
	public required string CommitUuid { get; set; }

	/// <summary>If true, the cache will be cleaned before the analysis</summary>
	public bool? CleanCache { get; set; }
}
