namespace Codacy.Api.Models;

/// <summary>
/// Response body for searching the repositories where a given dependency is used
/// </summary>
public class SearchRepositoriesOfSbomDependencyResponse
{
	/// <summary>Pagination info</summary>
	public required PaginationInfo Pagination { get; set; }

	/// <summary>The matching repositories</summary>
	public required List<RepositorySummaryOfSbomDependency> Data { get; set; }

	/// <summary>Overview of the dependency usage</summary>
	public required RepositoriesOverviewOfSbomDependency Overview { get; set; }
}
