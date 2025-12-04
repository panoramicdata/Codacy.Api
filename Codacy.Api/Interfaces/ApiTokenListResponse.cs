using Codacy.Api.Models;

namespace Codacy.Api.Interfaces;

/// <summary>
/// API token list response
/// </summary>
public class ApiTokenListResponse
{
	/// <summary>Pagination info</summary>
	public PaginationInfo? Pagination { get; set; }

	/// <summary>API tokens</summary>
	public required List<ApiToken> Data { get; set; }
}
