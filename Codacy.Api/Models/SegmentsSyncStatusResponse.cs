namespace Codacy.Api.Models;

/// <summary>
/// Status of the segments synchronization
/// </summary>
public class SegmentsSyncStatusResponse
{
	/// <summary>Status of the segments synchronization process. Valid values are Syncing, Completed, Error, NotSynced.</summary>
	public required string Status { get; set; }

	/// <summary>Error message, if any</summary>
	public string? Error { get; set; }
}
