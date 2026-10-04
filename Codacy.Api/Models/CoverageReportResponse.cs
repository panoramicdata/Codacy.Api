namespace Codacy.Api.Models;

/// <summary>
/// Coverage report response
/// </summary>
public class CoverageReportResponse
{
	/// <summary>Coverage report overview</summary>
	public required CoverageReportOverview Data { get; set; }
}
