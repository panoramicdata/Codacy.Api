namespace Codacy.Api.Models;

/// <summary>
/// Response containing a gate policy
/// </summary>
public class GetGatePolicyResultResponse
{
	/// <summary>The gate policy</summary>
	public required GatePolicy Data { get; set; }
}
