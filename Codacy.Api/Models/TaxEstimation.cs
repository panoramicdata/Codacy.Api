namespace Codacy.Api.Models;

/// <summary>
/// Tax estimation
/// </summary>
public class TaxEstimation
{
	/// <summary>Tax name</summary>
	public required string Name { get; set; }

	/// <summary>Tax rate</summary>
	public required double Rate { get; set; }

	/// <summary>Tax value in dollars</summary>
	public required double ValueDollars { get; set; }
}
