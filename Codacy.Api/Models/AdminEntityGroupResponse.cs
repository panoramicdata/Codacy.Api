namespace Codacy.Api.Models;

/// <summary>
/// Admin entity group response
/// </summary>
public class AdminEntityGroupResponse
{
	/// <summary>Entity groups</summary>
	public required List<AdminEntityGroup> Data { get; set; }
}
