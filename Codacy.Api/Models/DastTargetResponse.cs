namespace Codacy.Api.Models;

/// <summary>
/// A response with a target for DAST analysis
/// </summary>
public class DastTargetResponse
{
	/// <summary>The target</summary>
	public required DastTarget Data { get; set; }
}
