namespace Codacy.Api.Models;

/// <summary>
/// Payment card information
/// </summary>
public class Card
{
	/// <summary>Masked card number</summary>
	public required string MaskedNumber { get; set; }

	/// <summary>Last four digits</summary>
	public required string Last4 { get; set; }

	/// <summary>Expiry month</summary>
	public required int ExpiryMonth { get; set; }

	/// <summary>Expiry year</summary>
	public required int ExpiryYear { get; set; }

	/// <summary>Card holder name</summary>
	public required string HolderName { get; set; }
}
