namespace Codacy.Api.Models;

/// <summary>
/// Coverage information for a line of a file (spec schema FileCoverage, renamed to avoid the existing FileCoverage type)
/// </summary>
public class FileLineCoverage
{
	/// <summary>Line number</summary>
	public required int Line { get; set; }

	/// <summary>Number of test hits for the line</summary>
	public required int Hits { get; set; }
}
