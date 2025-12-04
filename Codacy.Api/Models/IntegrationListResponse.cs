namespace Codacy.Api.Models;

/// <summary>
/// Integration list response
/// </summary>
public class IntegrationListResponse
{
	/// <summary>Pagination info</summary>
	public PaginationInfo? Pagination { get; set; }

	/// <summary>Integrations</summary>
	public required List<Integration> Data { get; set; }
}
