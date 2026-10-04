namespace Codacy.Api.Models;

/// <summary>
/// Summary of the dependencies of a repository
/// </summary>
public class DependenciesSummaryOfSbomRepository
{
	/// <summary>The identifier of the repository</summary>
	public required long Id { get; set; }

	/// <summary>The name of the repository</summary>
	public required string Name { get; set; }

	/// <summary>Number of dependencies, including indirect ones</summary>
	public required int DependenciesCount { get; set; }

	/// <summary>Open findings count, per severity, for the dependencies of this repository</summary>
	public required List<OpenFindingsCount> DependenciesFindings { get; set; }
}
