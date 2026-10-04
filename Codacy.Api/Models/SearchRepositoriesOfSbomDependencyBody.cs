namespace Codacy.Api.Models;

/// <summary>
/// Request body for searching the repositories where a dependency is used
/// </summary>
public class SearchRepositoriesOfSbomDependencyBody
{
	/// <summary>The full name of the dependency to search for</summary>
	public required string DependencyFullName { get; set; }

	/// <summary>Repository names to filter by</summary>
	public List<string>? RepositoriesFilter { get; set; }
}
