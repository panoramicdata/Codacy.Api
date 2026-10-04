namespace Codacy.Api.Models;

/// <summary>
/// Feedback relating to an enriched tool pattern
/// </summary>
public class AddEnrichedPatternFeedbackBody
{
	/// <summary>True if the enriched pattern is considered good/relevant by the user</summary>
	public required bool ReactionFeedback { get; set; }

	/// <summary>Feedback from the user describing why the enriched pattern is not considered good/relevant</summary>
	public string? Feedback { get; set; }
}
