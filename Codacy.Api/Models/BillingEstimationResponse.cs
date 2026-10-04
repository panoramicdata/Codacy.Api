namespace Codacy.Api.Models;

/// <summary>
/// Billing estimation response
/// </summary>
public class BillingEstimationResponse
{
	/// <summary>Billing estimation</summary>
	public required BillingEstimation Data { get; set; }
}
