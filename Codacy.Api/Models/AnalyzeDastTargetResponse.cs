namespace Codacy.Api.Models;

/// <summary>
/// A response with the analysis ID that was queued
/// </summary>
public class AnalyzeDastTargetResponse
{
	/// <summary>The queued analysis</summary>
	public required AnalyzeDastTargetResult Data { get; set; }
}
