namespace Codacy.Api.Models;

/// <summary>
/// File with analysis info
/// </summary>
public class FileWithAnalysisInfo
{
	/// <summary>File ID</summary>
	public required long FileId { get; set; }

	/// <summary>Branch ID</summary>
	public required long BranchId { get; set; }

	/// <summary>File path</summary>
	public required string Path { get; set; }

	/// <summary>Total issues</summary>
	public required int TotalIssues { get; set; }

	/// <summary>Number of methods</summary>
	public required int NumberOfMethods { get; set; }

	/// <summary>Grade (0-100)</summary>
	public required int Grade { get; set; }

	/// <summary>Grade letter (A-F)</summary>
	public required string GradeLetter { get; set; }

	/// <summary>Complexity</summary>
	public int? Complexity { get; set; }

	/// <summary>Coverage with decimals</summary>
	public double? CoverageWithDecimals { get; set; }

	/// <summary>Lines of code</summary>
	public int? LinesOfCode { get; set; }
}
