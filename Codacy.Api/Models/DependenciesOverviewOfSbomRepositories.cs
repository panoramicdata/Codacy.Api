namespace Codacy.Api.Models;

/// <summary>
/// Overview of dependencies used across repositories
/// </summary>
public class DependenciesOverviewOfSbomRepositories
{
	/// <summary>Total repositories with dependency information (not affected by filtering)</summary>
	public required int TotalRepositoriesCount { get; set; }

	/// <summary>Filtered repositories with dependency information</summary>
	public required int FilteredRepositoriesCount { get; set; }

	/// <summary>Total dependencies across repositories (not affected by filtering)</summary>
	public required long TotalDependenciesCount { get; set; }

	/// <summary>Filtered dependencies across repositories</summary>
	public required long FilteredDependenciesCount { get; set; }
}
