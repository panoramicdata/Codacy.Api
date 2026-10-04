namespace Codacy.Api.Models;

/// <summary>
/// Paginated list of tools
/// </summary>
public class ToolListResponse
{
	/// <summary>Tools</summary>
	public required List<Tool> Data { get; set; }

	/// <summary>Pagination info</summary>
	public PaginationInfo? Pagination { get; set; }
}
