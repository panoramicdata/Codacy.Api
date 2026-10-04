namespace Codacy.Api.Models;

/// <summary>
/// Details of the gate policy
/// </summary>
public class GatePolicy
{
	/// <summary>Identifier of the gate policy</summary>
	public required long Id { get; set; }

	/// <summary>Name of the gate policy</summary>
	public required string Name { get; set; }

	/// <summary>True if the gate policy is the default for the organization</summary>
	public required bool IsDefault { get; set; }

	/// <summary>True if the quality gates of the gate policy cannot be changed</summary>
	public required bool ReadOnly { get; set; }

	/// <summary>Quality gate settings</summary>
	public required QualityGate Settings { get; set; }

	/// <summary>Meta information about the gate policy</summary>
	public required GatePolicyMeta Meta { get; set; }
}
