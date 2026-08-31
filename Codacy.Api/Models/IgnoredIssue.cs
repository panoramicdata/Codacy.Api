namespace Codacy.Api.Models;

/// <summary>
/// Ignored issue
/// </summary>
public class IgnoredIssue : IssueMetadata
{
	/// <summary>Ignore reason</summary>
	public string? Reason { get; set; }

	/// <summary>Comment</summary>
	public string? Comment { get; set; }

	/// <summary>Ignored by name</summary>
	public string? IgnoredByName { get; set; }

	/// <summary>Ignored timestamp</summary>
	public required DateTimeOffset IgnoredTimestamp { get; set; }

	/// <summary>File ID</summary>
	public long? FileId { get; set; }

	/// <summary>Line number</summary>
	public long? LineNumber { get; set; }

	/// <summary>False positive threshold</summary>
	public int? FalsePositiveThreshold { get; set; }
}