namespace Codacy.Api.Models;

/// <summary>
/// Organization billing information response
/// </summary>
public class OrganizationBillingInformationResponse
{
	/// <summary>Billing data</summary>
	public required OrganizationBillingInformation Data { get; set; }
}
