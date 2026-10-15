namespace Codacy.Api.Models;

/// <summary>
/// Common filters for AI inventory queries
/// </summary>
public class AiInventoryFilter : AiInventoryFilterBase
{
	/// <summary>AI provider name to filter by</summary>
	public string? AiProvider { get; set; }

	/// <summary>Marker text to filter by</summary>
	public string? Marker { get; set; }
}
