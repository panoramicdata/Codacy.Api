namespace Codacy.Api.Models;

/// <summary>
/// Status of the repository setting Run analysis on your build server
/// </summary>
public class BuildServerAnalysisSettingResponse
{
	/// <summary>If true, Codacy waits for the build server to upload local analysis results; if false, Codacy analyzes commits on its cloud infrastructure</summary>
	public required bool BuildServerAnalysisSetting { get; set; }
}
