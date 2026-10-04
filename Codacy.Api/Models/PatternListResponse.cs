namespace Codacy.Api.Models;

/// <summary>
/// Paginated list of tool patterns
/// </summary>
public class PatternListResponse
{
	/// <summary>Patterns</summary>
	public required List<Pattern> Data { get; set; }

	/// <summary>Pagination info</summary>
	public PaginationInfo? Pagination { get; set; }
}
