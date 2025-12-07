namespace Codacy.Api.Models;

/// <summary>
/// Generic paginated list response
/// </summary>
/// <typeparam name="T">The type of items in the list</typeparam>
public class ListResponse<T>
{
	/// <summary>Pagination info</summary>
	public PaginationInfo? Pagination { get; set; }

	/// <summary>List of items</summary>
	public required List<T> Data { get; set; }
}
