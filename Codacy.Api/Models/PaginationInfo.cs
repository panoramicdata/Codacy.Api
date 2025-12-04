namespace Codacy.Api.Models;

/// <summary>
/// Pagination information
/// </summary>
public class PaginationInfo
{
	/// <summary>Cursor for next page</summary>
	public string? Cursor { get; set; }
	/// <summary>Maximum number of items</summary>
	public int? Limit { get; set; }
	/// <summary>Total number of items</summary>
	public int? Total { get; set; }
}
