namespace Codacy.Api.Models;

/// <summary>
/// Ignored issue
/// </summary>
public class IgnoredIssue
{
	/// <summary>Issue ID</summary>
	public required string IssueId { get; set; }

	/// <summary>Ignore reason</summary>
	public string? Reason { get; set; }

	/// <summary>Comment</summary>
	public string? Comment { get; set; }

	/// <summary>Ignored by name</summary>
	public string? IgnoredByName { get; set; }

	/// <summary>Ignored timestamp</summary>
	public required DateTimeOffset IgnoredTimestamp { get; set; }

	/// <summary>File path</summary>
	public required string FilePath { get; set; }

	/// <summary>File ID</summary>
	public long? FileId { get; set; }

	/// <summary>Pattern info</summary>
	public required PatternDetails PatternInfo { get; set; }

	/// <summary>Tool info</summary>
	public required ToolReference ToolInfo { get; set; }

	/// <summary>Line number</summary>
	public long? LineNumber { get; set; }

	/// <summary>Message</summary>
	public required string Message { get; set; }

	/// <summary>Language</summary>
	public required string Language { get; set; }

	/// <summary>Line text</summary>
	public required string LineText { get; set; }

	/// <summary>Commit info</summary>
	public CommitReference? CommitInfo { get; set; }

	/// <summary>False positive probability</summary>
	public int? FalsePositiveProbability { get; set; }

	/// <summary>False positive reason</summary>
	public string? FalsePositiveReason { get; set; }

	/// <summary>False positive threshold</summary>
	public int? FalsePositiveThreshold { get; set; }
}
