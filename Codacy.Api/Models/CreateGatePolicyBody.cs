namespace Codacy.Api.Models;

/// <summary>
/// Details of a new gate policy
/// </summary>
public class CreateGatePolicyBody
{
	/// <summary>Name of the gate policy</summary>
	public required string GatePolicyName { get; set; }

	/// <summary>True if the gate policy is the default for the organization</summary>
	public bool? IsDefault { get; set; }

	/// <summary>Quality gate settings</summary>
	public QualityGate? Settings { get; set; }
}
