namespace Codacy.Api.Models;

/// <summary>
/// Restricts a metric query to specific repositories or segments
/// </summary>
public class EntityFilter
{
	/// <summary>Repository names</summary>
	public List<string>? Repositories { get; set; }

	/// <summary>Segment identifiers</summary>
	public List<long>? SegmentIds { get; set; }
}
