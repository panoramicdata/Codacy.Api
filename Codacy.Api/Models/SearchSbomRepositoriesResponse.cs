namespace Codacy.Api.Models;

/// <summary>
/// Response body for searching repositories with dependency information
/// </summary>
public class SearchSbomRepositoriesResponse
{
	/// <summary>Pagination info</summary>
	public required PaginationInfo Pagination { get; set; }

	/// <summary>The matching repositories</summary>
	public required List<DependenciesSummaryOfSbomRepository> Data { get; set; }

	/// <summary>Overview of the search</summary>
	public required DependenciesOverviewOfSbomRepositories Overview { get; set; }
}
