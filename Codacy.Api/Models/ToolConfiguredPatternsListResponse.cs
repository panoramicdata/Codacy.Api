namespace Codacy.Api.Models;

/// <summary>
/// Paginated list of code patterns configured for a tool
/// </summary>
public class ToolConfiguredPatternsListResponse
{
	/// <summary>Configured patterns</summary>
	public required List<ToolConfiguredPattern> Data { get; set; }

	/// <summary>Pagination info</summary>
	public PaginationInfo? Pagination { get; set; }

	/// <summary>Metadata for the retrieved pattern list</summary>
	public ToolConfiguredPatternListMeta? Meta { get; set; }
}
