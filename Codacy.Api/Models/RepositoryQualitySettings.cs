namespace Codacy.Api.Models;

/// <summary>
/// Repository quality settings
/// </summary>
public class RepositoryQualitySettings
{
	/// <summary>Max issue percentage</summary>
	public int? MaxIssuePercentage { get; set; }

	/// <summary>Max duplicated files percentage</summary>
	public int? MaxDuplicatedFilesPercentage { get; set; }

	/// <summary>Min coverage percentage</summary>
	public int? MinCoveragePercentage { get; set; }

	/// <summary>Max complex files percentage</summary>
	public int? MaxComplexFilesPercentage { get; set; }

	/// <summary>File duplication block threshold</summary>
	public int? FileDuplicationBlockThreshold { get; set; }

	/// <summary>File complexity value threshold</summary>
	public int? FileComplexityValueThreshold { get; set; }
}
