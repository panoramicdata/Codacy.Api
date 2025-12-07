namespace Codacy.Api.Models;

/// <summary>
/// Quality settings with gate policy
/// </summary>
public class QualitySettingsWithGatePolicy
{
	/// <summary>Quality gate</summary>
	public required QualityGate QualityGate { get; set; }

	/// <summary>Repository gate policy info</summary>
	public RepositoryGatePolicy? RepositoryGatePolicyInfo { get; set; }
}
