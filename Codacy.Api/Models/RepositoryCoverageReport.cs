namespace Codacy.Api.Models;

/// <summary>
/// Status and details of a coverage report (spec schema CoverageReport, renamed to avoid the existing CoverageReport type)
/// </summary>
public class RepositoryCoverageReport
{
	/// <summary>Commit SHA that was referenced as the target for this report</summary>
	public required string TargetCommitSha { get; set; }

	/// <summary>Commit details</summary>
	public CommitWithBranches? Commit { get; set; }

	/// <summary>Programming language associated with the coverage report</summary>
	public string? Language { get; set; }

	/// <summary>Report creation date</summary>
	public required DateTimeOffset CreatedAt { get; set; }

	/// <summary>Coverage status</summary>
	public required CoverageReportStatus Status { get; set; }
}
