namespace Codacy.Api.Models;

/// <summary>
/// Response body for searching dependencies
/// </summary>
public class SearchSbomDependenciesResponse
{
	/// <summary>Pagination info</summary>
	public required PaginationInfo Pagination { get; set; }

	/// <summary>The matching dependencies</summary>
	public required List<SbomDependencySummary> Data { get; set; }

	/// <summary>Overview of the search</summary>
	public required SbomDependenciesOverview Overview { get; set; }
}
