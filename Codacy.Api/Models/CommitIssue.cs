namespace Codacy.Api.Models;

/// <summary>
/// Issue details including the commit that originated the issue
/// </summary>
public class CommitIssue
{
	/// <summary>ID of the issue</summary>
	public required string IssueId { get; set; }

	/// <summary>Stable ID for the issue</summary>
	public required long ResultDataId { get; set; }

	/// <summary>Path of the file where the issue was found</summary>
	public required string FilePath { get; set; }

	/// <summary>File ID</summary>
	public required long FileId { get; set; }

	/// <summary>Pattern information</summary>
	public required PatternDetails PatternInfo { get; set; }

	/// <summary>Tool information</summary>
	public required ToolReference ToolInfo { get; set; }

	/// <summary>Line where the issue was found</summary>
	public required long LineNumber { get; set; }

	/// <summary>Detailed cause of the issue</summary>
	public required string Message { get; set; }

	/// <summary>The suggested fix for the issue</summary>
	public string? Suggestion { get; set; }

	/// <summary>Language of the file where the issue was found</summary>
	public required string Language { get; set; }

	/// <summary>Contents of the line where the issue was found</summary>
	public required string LineText { get; set; }

	/// <summary>Commit that introduced the issue</summary>
	public CommitReference? CommitInfo { get; set; }

	/// <summary>Probability that the issue is a false positive</summary>
	public int? FalsePositiveProbability { get; set; }

	/// <summary>Reasoning for the false positive</summary>
	public string? FalsePositiveReason { get; set; }

	/// <summary>Threshold for false positive detection</summary>
	public required int FalsePositiveThreshold { get; set; }

	/// <summary>Advisory information, only present for SCA issues</summary>
	public AdvisoryInformation? AdvisoryInformation { get; set; }

	/// <summary>Dependency chains from the root package to the vulnerable package, only present for SCA findings</summary>
	public List<List<string>>? DependencyChains { get; set; }

	/// <summary>Versions that fix the vulnerable dependency, empty when no fix is available, only present for SCA issues</summary>
	public List<string>? FixedVersion { get; set; }
}
