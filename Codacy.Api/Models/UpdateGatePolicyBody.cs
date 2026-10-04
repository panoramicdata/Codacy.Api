namespace Codacy.Api.Models;

/// <summary>
/// New values for a gate policy
/// </summary>
public class UpdateGatePolicyBody
{
	/// <summary>Name of the gate policy</summary>
	public string? GatePolicyName { get; set; }

	/// <summary>True if the gate policy is the default for the organization</summary>
	public bool? IsDefault { get; set; }

	/// <summary>Quality gate settings</summary>
	public QualityGate? Settings { get; set; }
}
