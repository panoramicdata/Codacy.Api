namespace Codacy.Api.Models;

/// <summary>
/// Detailed information about a license
/// </summary>
public class LicensesDetails
{
	/// <summary>The name of the license</summary>
	public string? Name { get; set; }

	/// <summary>The official page of the license</summary>
	public string? Url { get; set; }

	/// <summary>Whether the license is OSI approved</summary>
	public bool? IsOsiApproved { get; set; }

	/// <summary>Whether the license is FSF libre</summary>
	public bool? IsFsfLibre { get; set; }

	/// <summary>The license risk category</summary>
	public LicenseRiskCategory? RiskCategory { get; set; }

	/// <summary>Whether the license details were derived by AI</summary>
	public bool? IsDerivedByAi { get; set; }
}
