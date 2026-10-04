namespace Codacy.Api.Models;

/// <summary>
/// Coverage report for a file
/// </summary>
public class CoverageReportFile
{
	/// <summary>Name of the file</summary>
	public required string FileName { get; set; }

	/// <summary>Coverage map from line number to number of hits</summary>
	public required Dictionary<string, int> Coverage { get; set; }
}
