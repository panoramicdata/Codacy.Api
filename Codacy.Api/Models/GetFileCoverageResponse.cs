namespace Codacy.Api.Models;

/// <summary>
/// File coverage response
/// </summary>
public class GetFileCoverageResponse
{
	/// <summary>Coverage per line</summary>
	public required List<FileLineCoverage> Data { get; set; }
}
