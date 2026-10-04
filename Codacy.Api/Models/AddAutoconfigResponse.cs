namespace Codacy.Api.Models;

/// <summary>
/// A response confirming the autoconfig run was accepted
/// </summary>
public class AddAutoconfigResponse
{
	/// <summary>Accepted run details</summary>
	public required AddAutoconfigData Data { get; set; }
}

/// <summary>
/// Details of an accepted autoconfig run
/// </summary>
public class AddAutoconfigData
{
	/// <summary>The autoconfig analysis id that was queued</summary>
	public required Guid AnalysisId { get; set; }
}
