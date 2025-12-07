namespace Codacy.Api.Models;

/// <summary>
/// File quality info
/// </summary>
public class FileQualityInfo
{
	/// <summary>Total issues</summary>
	public required int TotalIssues { get; set; }

	/// <summary>Grade (0-100)</summary>
	public required int Grade { get; set; }

	/// <summary>Grade letter (A-F)</summary>
	public required string GradeLetter { get; set; }

	/// <summary>Complexity</summary>
	public int? Complexity { get; set; }

	/// <summary>Duplication</summary>
	public int? Duplication { get; set; }

	/// <summary>Duplicated lines of code</summary>
	public int? DuplicatedLinesOfCode { get; set; }
}
