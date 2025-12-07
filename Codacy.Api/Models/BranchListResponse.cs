namespace Codacy.Api.Models;

/// <summary>
/// Branch list response
/// </summary>
public class BranchListResponse
{
	/// <summary>Pagination info</summary>
	public PaginationInfo? Pagination { get; set; }

	/// <summary>Branches</summary>
	public required List<Branch> Data { get; set; }
}
