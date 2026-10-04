namespace Codacy.Api.Models;

/// <summary>
/// Entry of a coverage report uploaded for a commit
/// </summary>
public class CoverageReportEntry
{
	/// <summary>Repository ID</summary>
	public required long RepositoryId { get; set; }

	/// <summary>Commit SHA</summary>
	public required string CommitSha { get; set; }

	/// <summary>Report ID</summary>
	public required string ReportId { get; set; }

	/// <summary>Language of the report</summary>
	public string? Language { get; set; }

	/// <summary>Whether this is the final report</summary>
	public required bool IsFinal { get; set; }

	/// <summary>Whether the report was processed</summary>
	public required bool Processed { get; set; }

	/// <summary>Report creation date</summary>
	public required DateTimeOffset CreatedAt { get; set; }
}
