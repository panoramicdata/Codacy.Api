namespace Codacy.Api.Models;

/// <summary>
/// Issue details including the commit that originated the issue
/// </summary>
/// <remarks>
/// Everything an <see cref="Issue"/> carries, including the originating <see cref="IssueMetadata.CommitInfo"/>,
/// plus what Codacy adds when asked for the issues of a file.
/// </remarks>
public class CommitIssue : Issue
{
	/// <summary>Advisory information, only present for SCA issues</summary>
	public AdvisoryInformation? AdvisoryInformation { get; set; }

	/// <summary>Dependency chains from the root package to the vulnerable package, only present for SCA findings</summary>
	public List<List<string>>? DependencyChains { get; set; }

	/// <summary>Versions that fix the vulnerable dependency, empty when no fix is available, only present for SCA issues</summary>
	public List<string>? FixedVersion { get; set; }
}
