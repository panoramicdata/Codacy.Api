namespace Codacy.Api.Models;

/// <summary>
/// AI Risk Checklist for an organization
/// </summary>
public class AiRiskChecklistResponse
{
	/// <summary>Checklist items</summary>
	public required List<AiRiskCheckListItem> Data { get; set; }
}
