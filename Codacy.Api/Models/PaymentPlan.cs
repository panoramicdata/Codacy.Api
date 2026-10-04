namespace Codacy.Api.Models;

/// <summary>
/// Payment plan
/// </summary>
public class PaymentPlan
{
	/// <summary>Whether the plan is premium</summary>
	public required bool IsPremium { get; set; }

	/// <summary>Plan model</summary>
	public required PaymentPlanModel Model { get; set; }

	/// <summary>Plan code</summary>
	public required string Code { get; set; }

	/// <summary>Whether the plan is billed monthly</summary>
	public required bool Monthly { get; set; }

	/// <summary>Price</summary>
	public required long Price { get; set; }

	/// <summary>Whether the plan is priced per user</summary>
	public required bool PricedPerUser { get; set; }

	/// <summary>Plan code for the same tier with opposite billing period (monthly or yearly)</summary>
	public string? AlternatePeriodCode { get; set; }
}
