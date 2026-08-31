namespace Codacy.Api.Test;

/// <summary>
/// Test environment status information
/// </summary>
public class TestEnvironmentStatus
{
	/// <summary>Organization name</summary>
	public required string Organization { get; init; }

	/// <summary>Repository name</summary>
	public required string Repository { get; init; }

	/// <summary>Provider</summary>
	public required Provider Provider { get; init; }

	/// <summary>Whether the repository exists in Codacy</summary>
	public bool RepositoryExists { get; set; }

	/// <summary>Whether the repository has analysis data</summary>
	public bool HasAnalysisData { get; set; }

	/// <summary>Whether the repository has branches</summary>
	public bool HasBranches { get; set; }

	/// <summary>Number of branches</summary>
	public int BranchCount { get; set; }

	/// <summary>Number of files</summary>
	public int FileCount { get; set; }

	/// <summary>Error message if status check failed</summary>
	public string? ErrorMessage { get; set; }

	/// <summary>Whether the environment is ready for testing</summary>
	public bool IsReady => RepositoryExists && HasAnalysisData && HasBranches;

	/// <summary>Gets a summary of the environment status</summary>
	public override string ToString() => string.IsNullOrEmpty(ErrorMessage)
		? $"Repository: {Provider}/{Organization}/{Repository} | " +
		  $"Exists: {RepositoryExists} | " +
		  $"Analyzed: {HasAnalysisData} | " +
		  $"Branches: {BranchCount} | " +
		  $"Files: {FileCount} | " +
		  $"Ready: {IsReady}"
		: $"Error: {ErrorMessage}";
}