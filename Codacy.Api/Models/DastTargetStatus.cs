namespace Codacy.Api.Models;

/// <summary>
/// The current analysis status of a DAST target
/// </summary>
public class DastTargetStatus
{
	/// <summary>The current analysis status</summary>
	public required DastAnalysisStatus Status { get; set; }

	/// <summary>When the target transitioned to this status</summary>
	public required DateTimeOffset TransitionedAt { get; set; }

	/// <summary>An optional description of why the target transitioned to the current status</summary>
	public string? TransitionReason { get; set; }

	/// <summary>The DAST analysis id associated with this status</summary>
	public required Guid AnalysisId { get; set; }
}
