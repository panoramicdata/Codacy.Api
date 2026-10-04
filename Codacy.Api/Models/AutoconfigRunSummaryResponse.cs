namespace Codacy.Api.Models;

/// <summary>
/// The latest autoconfig run summary for a repository
/// </summary>
public class AutoconfigRunSummaryResponse
{
	/// <summary>Run summary</summary>
	public required AutoconfigRunSummary Data { get; set; }
}
