namespace Codacy.Api.Models;

/// <summary>
/// Required AI provider identity plus optional filters scoping the returned summary
/// </summary>
public class GetAiInventoryProviderSummaryBody : AiInventoryFilterBase
{
	/// <summary>AI provider name to return the summary for</summary>
	public required string AiProvider { get; set; }
}
