namespace Codacy.Api.Models;

/// <summary>
/// Billing estimation
/// </summary>
public class BillingEstimation
{
	/// <summary>Price per seat in cents</summary>
	public required int PerSeatCents { get; set; }

	/// <summary>Number of seats</summary>
	public required int Seats { get; set; }

	/// <summary>Taxes</summary>
	public required List<TaxEstimation> Taxes { get; set; }

	/// <summary>Discount in cents</summary>
	public int? DiscountCents { get; set; }

	/// <summary>Subtotal in cents</summary>
	public required long SubTotalCents { get; set; }

	/// <summary>Total in cents</summary>
	public required long TotalCents { get; set; }

	/// <summary>Next billing date</summary>
	public required DateTimeOffset NextBilling { get; set; }

	/// <summary>Whether billing is monthly</summary>
	public required bool IsMonthly { get; set; }
}
