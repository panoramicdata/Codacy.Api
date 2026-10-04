namespace Codacy.Api.Models;

/// <summary>
/// Payment plans response
/// </summary>
public class PaymentPlansResponse
{
	/// <summary>Payment plans</summary>
	public required CodacyPaymentPlans Data { get; set; }
}
