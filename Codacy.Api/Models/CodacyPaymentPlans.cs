namespace Codacy.Api.Models;

/// <summary>
/// Available payment plans
/// </summary>
public class CodacyPaymentPlans
{
	/// <summary>Default yearly paid plan code</summary>
	public required string DefaultYearlyPaidCode { get; set; }

	/// <summary>Default monthly paid plan code</summary>
	public required string DefaultMonthlyPaidCode { get; set; }

	/// <summary>Trial plan code</summary>
	public required string TrialCode { get; set; }

	/// <summary>Open source plan code</summary>
	public required string OpenSourceCode { get; set; }

	/// <summary>Default yearly paid plan</summary>
	public required PaymentPlan DefaultYearlyPaidPlan { get; set; }

	/// <summary>Default monthly paid plan</summary>
	public required PaymentPlan DefaultMonthlyPaidPlan { get; set; }

	/// <summary>Trial plan</summary>
	public required PaymentPlan TrialPlan { get; set; }

	/// <summary>Open source plan</summary>
	public required PaymentPlan OpenSourcePlan { get; set; }

	/// <summary>All plans</summary>
	public required List<PaymentPlan> Plans { get; set; }
}
