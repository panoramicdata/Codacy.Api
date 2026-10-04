namespace Codacy.Api.Models;

/// <summary>
/// Summary of a repository where a given dependency is used
/// </summary>
public class RepositorySummaryOfSbomDependency
{
	/// <summary>The identifier of the repository</summary>
	public required long Id { get; set; }

	/// <summary>The name of the repository</summary>
	public required string Name { get; set; }

	/// <summary>The exact version of the dependency this repository is using</summary>
	public string? DependencyVersion { get; set; }

	/// <summary>The highest severity of the findings for this dependency in this repository</summary>
	public string? HighestFindingSeverity { get; set; }

	/// <summary>Deprecated upstream, use LicensesDetails instead</summary>
	public required List<string> Licenses { get; set; }

	/// <summary>Detailed license information for the dependency used in this repository</summary>
	public required List<LicensesDetails> LicensesDetails { get; set; }
}
