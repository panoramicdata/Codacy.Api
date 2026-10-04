namespace Codacy.Api.Models;

/// <summary>
/// Response containing the summary for one AI provider
/// </summary>
public class AiInventoryProviderSummaryResponse
{
	/// <summary>The provider summary</summary>
	public required AiInventoryProviderSummary Data { get; set; }
}
