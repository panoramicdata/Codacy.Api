namespace Codacy.Api.Models;

/// <summary>
/// Folder with analysis information
/// </summary>
public class DirectoryWithAnalysisInfo
{
	/// <summary>Full path of the folder in the repository</summary>
	public required string Path { get; set; }

	/// <summary>Name of the folder, that is the last segment of its path</summary>
	public required string Name { get; set; }

	/// <summary>Number of files in the folder, including all subfolders</summary>
	public required int NrFiles { get; set; }

	/// <summary>Number of issues in the folder, including all subfolders</summary>
	public required int TotalIssues { get; set; }

	/// <summary>Quality grade of the folder between 100 (highest) and 0 (lowest)</summary>
	public required int Grade { get; set; }

	/// <summary>Quality grade of the folder as a letter between A (highest) and F (lowest)</summary>
	public required string GradeLetter { get; set; }

	/// <summary>Highest complexity of a file in the folder</summary>
	public int? Complexity { get; set; }

	/// <summary>Total complexity of all files in the folder</summary>
	public int? ComplexitySum { get; set; }

	/// <summary>Number of duplicated lines in the folder</summary>
	public int? Duplication { get; set; }

	/// <summary>Number of cloned blocks of code in the folder</summary>
	public int? NumberOfClones { get; set; }

	/// <summary>Test coverage percentage of the folder with decimals</summary>
	public double? CoverageWithDecimals { get; set; }

	/// <summary>Coverable lines of code in the folder</summary>
	public int? SourceLinesOfCode { get; set; }

	/// <summary>Lines of code in the folder</summary>
	public int? LinesOfCode { get; set; }
}
