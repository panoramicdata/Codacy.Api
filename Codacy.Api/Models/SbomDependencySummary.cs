namespace Codacy.Api.Models;

/// <summary>
/// Summary of a dependency
/// </summary>
public class SbomDependencySummary
{
	/// <summary>The full name of the dependency</summary>
	public required string FullName { get; set; }

	/// <summary>The purl of the dependency</summary>
	public string? Purl { get; set; }

	/// <summary>The ossf score of the dependency</summary>
	public double? OssfScore { get; set; }

	/// <summary>The number of repositories where a version of this dependency is used</summary>
	public required int RepositoriesCount { get; set; }

	/// <summary>The number of versions this dependency has across repositories</summary>
	public required int VersionsCount { get; set; }

	/// <summary>Open findings count, per severity, found for this dependency</summary>
	public required List<OpenFindingsCount> Findings { get; set; }

	/// <summary>Detailed license information for this dependency</summary>
	public required List<LicensesDetails> LicensesDetails { get; set; }
}
