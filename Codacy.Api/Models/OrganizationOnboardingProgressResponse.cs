namespace Codacy.Api.Models;

/// <summary>
/// Onboarding progress response
/// </summary>
public class OrganizationOnboardingProgressResponse
{
	/// <summary>Completeness status of each onboarding step</summary>
	public required List<OrganizationOnboardingStep> Data { get; set; }
}
