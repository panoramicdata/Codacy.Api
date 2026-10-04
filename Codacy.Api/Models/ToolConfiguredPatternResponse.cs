namespace Codacy.Api.Models;

/// <summary>
/// Response containing a single configured code pattern
/// </summary>
public class ToolConfiguredPatternResponse
{
	/// <summary>Configured pattern</summary>
	public required ToolConfiguredPattern Data { get; set; }
}
