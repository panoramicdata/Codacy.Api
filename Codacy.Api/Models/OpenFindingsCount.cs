namespace Codacy.Api.Models;

/// <summary>
/// The open findings count for a given severity
/// </summary>
public class OpenFindingsCount
{
	/// <summary>The severity of the findings</summary>
	public required string Severity { get; set; }

	/// <summary>The number of open findings</summary>
	public required int Open { get; set; }
}
