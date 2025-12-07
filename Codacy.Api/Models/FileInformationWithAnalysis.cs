namespace Codacy.Api.Models;

/// <summary>
/// File information with analysis
/// </summary>
public class FileInformationWithAnalysis
{
	/// <summary>File metadata</summary>
	public required FileMetadata File { get; set; }

	/// <summary>File metrics</summary>
	public FileMetrics? Metrics { get; set; }

	/// <summary>Coverage analysis</summary>
	public FileCoverageAnalysis? Coverage { get; set; }

	/// <summary>Quality info</summary>
	public FileQualityInfo? Quality { get; set; }
}
