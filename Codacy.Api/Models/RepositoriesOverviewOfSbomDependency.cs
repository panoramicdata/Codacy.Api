namespace Codacy.Api.Models;

/// <summary>
/// Overview of how a dependency is used across repositories
/// </summary>
public class RepositoriesOverviewOfSbomDependency
{
	/// <summary>The dependency name</summary>
	public string? Name { get; set; }

	/// <summary>The dependency full name</summary>
	public required string FullName { get; set; }

	/// <summary>The purl of the dependency</summary>
	public string? Purl { get; set; }

	/// <summary>The ossf score of the dependency</summary>
	public double? OssfScore { get; set; }

	/// <summary>Latest version used across filtered repositories</summary>
	public string? LatestVersion { get; set; }

	/// <summary>Oldest version used across filtered repositories</summary>
	public string? OldestVersion { get; set; }

	/// <summary>Versions in use across repositories (not affected by filtering)</summary>
	public int? TotalVersionsCount { get; set; }

	/// <summary>Versions in use across the filtered repositories</summary>
	public required int FilteredVersionsCount { get; set; }

	/// <summary>Repositories using a version of this dependency (not affected by filtering)</summary>
	public required int TotalRepositoriesCount { get; set; }

	/// <summary>Filtered repositories using a version of this dependency</summary>
	public required int FilteredRepositoriesCount { get; set; }
}
