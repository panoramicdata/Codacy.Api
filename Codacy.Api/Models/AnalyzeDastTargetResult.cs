namespace Codacy.Api.Models;

/// <summary>
/// The queued DAST analysis
/// </summary>
public class AnalyzeDastTargetResult
{
	/// <summary>The identifier of the queued analysis</summary>
	public required Guid AnalysisId { get; set; }
}
