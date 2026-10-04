namespace Codacy.Api.Models;

/// <summary>
/// The latest autoconfig run status for the repository
/// </summary>
public class AutoconfigStatusResponse
{
	/// <summary>Status details</summary>
	public required AutoconfigStatusData Data { get; set; }
}

/// <summary>
/// Autoconfig run status details
/// </summary>
public class AutoconfigStatusData
{
	/// <summary>The current autoconfig analysis status of a repository</summary>
	public required AutoconfigStatus Status { get; set; }

	/// <summary>When the repository transitioned to this status</summary>
	public required DateTimeOffset TransitionedAt { get; set; }

	/// <summary>An optional description of why the repository transitioned to the current status</summary>
	public string? TransitionReason { get; set; }

	/// <summary>The autoconfig analysis id associated with this status</summary>
	public required Guid AnalysisId { get; set; }
}
