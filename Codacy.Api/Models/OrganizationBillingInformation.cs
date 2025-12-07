namespace Codacy.Api.Models;

/// <summary>
/// Organization billing information
/// </summary>
public class OrganizationBillingInformation
{
	/// <summary>Number of seats</summary>
	public required int NumberOfSeats { get; set; }

	/// <summary>Number of purchased seats</summary>
	public required int NumberOfPurchasedSeats { get; set; }

	/// <summary>Price in cents</summary>
	public required int PriceInCents { get; set; }

	/// <summary>Price per seat in cents</summary>
	public int? PricePerSeatInCents { get; set; }

	/// <summary>Next payment date</summary>
	public DateTimeOffset? NextPaymentDate { get; set; }
}
