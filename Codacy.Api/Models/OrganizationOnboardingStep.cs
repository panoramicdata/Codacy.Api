namespace Codacy.Api.Models;

/// <summary>
/// Onboarding step status
/// </summary>
public class OrganizationOnboardingStep
{
	/// <summary>Identifier of the onboarding step</summary>
	public required string Step { get; set; }

	/// <summary>Whether the step is completed</summary>
	public required bool IsCompleted { get; set; }
}
