namespace Codacy.Api.Models;

/// <summary>
/// Summary for a distinct location where an AI inventory item is referenced
/// </summary>
public class AiInventoryLocationSummary
{
	/// <summary>Location URI (for example repo-file:src/somefile.py)</summary>
	public required string Location { get; set; }

	/// <summary>Region URIs within the location (for example line:10); may be empty</summary>
	public required List<string> Regions { get; set; }
}
