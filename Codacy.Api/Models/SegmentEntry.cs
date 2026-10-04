namespace Codacy.Api.Models;

/// <summary>
/// Segment of an organization
/// </summary>
public class SegmentEntry
{
	/// <summary>Identifier of the segment</summary>
	public required long Id { get; set; }

	/// <summary>Name of the segment. Specific to the organization.</summary>
	public required string Name { get; set; }

	/// <summary>Value of the segment</summary>
	public string? Value { get; set; }

	/// <summary>Description of the segment</summary>
	public string? Description { get; set; }
}
