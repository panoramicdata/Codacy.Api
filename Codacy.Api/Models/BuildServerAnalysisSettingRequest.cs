namespace Codacy.Api.Models;

/// <summary>
/// New value for the repository setting Run analysis on your build server
/// </summary>
public class BuildServerAnalysisSettingRequest
{
	/// <summary>If true, Codacy waits for the build server to upload local analysis results; if false, Codacy analyzes commits on its cloud infrastructure</summary>
	public required bool BuildServerAnalysisSetting { get; set; }
}
