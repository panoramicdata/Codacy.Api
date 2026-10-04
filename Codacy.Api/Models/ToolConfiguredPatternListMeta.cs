namespace Codacy.Api.Models;

/// <summary>
/// Metadata for a retrieved pattern list
/// </summary>
public class ToolConfiguredPatternListMeta
{
	/// <summary>Total number of enabled patterns</summary>
	public required int TotalEnabled { get; set; }
}
