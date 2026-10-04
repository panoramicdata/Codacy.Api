namespace Codacy.Api.Models;

/// <summary>
/// An item in the AI Risk Checklist
/// </summary>
public class AiRiskCheckListItem
{
	/// <summary>The key identifier for the checklist item</summary>
	public required string Key { get; set; }

	/// <summary>Checks if the checklist item passes</summary>
	public required bool Check { get; set; }

	/// <summary>The number of repositories that have the AI policy applied</summary>
	public int? Number { get; set; }

	/// <summary>The minimum percentage of repositories required to meet the AI policy compliance</summary>
	public int? Threshold { get; set; }

	/// <summary>The total number of repositories</summary>
	public int? Total { get; set; }
}
