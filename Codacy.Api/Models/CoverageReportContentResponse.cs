namespace Codacy.Api.Models;

/// <summary>
/// Coverage report content response
/// </summary>
public class CoverageReportContentResponse
{
	/// <summary>Report content</summary>
	public required CoverageReportContent Data { get; set; }
}
