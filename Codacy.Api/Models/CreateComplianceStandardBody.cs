namespace Codacy.Api.Models;

/// <summary>
/// Details required to create a compliance standard
/// </summary>
public class CreateComplianceStandardBody
{
	/// <summary>Name of the compliance standard</summary>
	public required string Name { get; set; }

	/// <summary>The type of compliance standard</summary>
	public required ComplianceType ComplianceType { get; set; }
}
