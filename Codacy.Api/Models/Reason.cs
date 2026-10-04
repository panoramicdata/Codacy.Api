namespace Codacy.Api.Models;

/// <summary>
/// Reason with notes
/// </summary>
public class Reason
{
	/// <summary>Title</summary>
	public required string Title { get; set; }

	/// <summary>Notes</summary>
	public required List<string> Notes { get; set; }
}
