namespace Codacy.Api.Models;

/// <summary>
/// Meta information about a gate policy
/// </summary>
public class GatePolicyMeta
{
	/// <summary>Number of quality gates that are configured in the gate policy</summary>
	public required int NrOfQualityGates { get; set; }

	/// <summary>Number of repositories following the gate policy</summary>
	public required int LinkedRepositoriesCount { get; set; }
}
