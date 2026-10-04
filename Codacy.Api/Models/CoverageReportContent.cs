namespace Codacy.Api.Models;

/// <summary>
/// Coverage report entry including its content
/// </summary>
public class CoverageReportContent : CoverageReportEntry
{
	/// <summary>Coverage report per file</summary>
	public required List<CoverageReportFile> Content { get; set; }
}
