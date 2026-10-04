namespace Codacy.Api.Models;

/// <summary>
/// List of duplication tools
/// </summary>
public class DuplicationToolListResponse
{
	/// <summary>Duplication tools</summary>
	public required List<DuplicationTool> Data { get; set; }
}
