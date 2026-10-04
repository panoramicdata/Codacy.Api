namespace Codacy.Api.Models;

/// <summary>
/// Segment key and id pair
/// </summary>
public class SegmentKeyWithId
{
	/// <summary>Segment id</summary>
	public required long Id { get; set; }

	/// <summary>Segment key</summary>
	public required string Key { get; set; }
}
