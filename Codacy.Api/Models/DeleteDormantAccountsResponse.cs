namespace Codacy.Api.Models;

/// <summary>
/// Delete dormant accounts response
/// </summary>
public class DeleteDormantAccountsResponse
{
	/// <summary>Deleted accounts</summary>
	public required List<DormantAccountInfo> Data { get; set; }
}
