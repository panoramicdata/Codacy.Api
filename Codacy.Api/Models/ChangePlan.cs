namespace Codacy.Api.Models;

/// <summary>
/// Request body to change the plan of an organization
/// </summary>
public class ChangePlan
{
	/// <summary>The code that uniquely identifies the payment plan</summary>
	public required string Code { get; set; }

	/// <summary>Promotional code</summary>
	public string? PromoCode { get; set; }
}
