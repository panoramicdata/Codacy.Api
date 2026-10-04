namespace Codacy.Api.Models;

/// <summary>
/// Cursor-based pagination information with a 64-bit total
/// </summary>
public class PaginationInfoLong
{
	/// <summary>Cursor to request the next batch of results</summary>
	public string? Cursor { get; set; }

	/// <summary>Maximum number of items returned</summary>
	public int? Limit { get; set; }

	/// <summary>Total number of items returned</summary>
	public long? Total { get; set; }
}
