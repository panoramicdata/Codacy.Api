namespace Codacy.Api.Models;

/// <summary>
/// Billing details update
/// </summary>
public class BillingDetailsUpdate
{
	/// <summary>First name</summary>
	public required string FirstName { get; set; }

	/// <summary>Last name</summary>
	public required string LastName { get; set; }

	/// <summary>Billing email</summary>
	public required string BillingEmail { get; set; }

	/// <summary>Country</summary>
	public required string Country { get; set; }

	/// <summary>VAT number</summary>
	public required string Vat { get; set; }

	/// <summary>Address</summary>
	public required string Address { get; set; }

	/// <summary>Zip or postal code</summary>
	public required string Zip { get; set; }

	/// <summary>State</summary>
	public required string State { get; set; }
}
