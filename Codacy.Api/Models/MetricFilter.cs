using System.Text.Json.Serialization;

namespace Codacy.Api.Models;

/// <summary>
/// Filter for an organization metric query
/// </summary>
public class MetricFilter
{
	/// <summary>Entities to include</summary>
	public required EntityFilter EntityFilter { get; set; }

	/// <summary>Dimension filters</summary>
	[JsonPropertyName("dimensionsFilter")]
	public List<DimensionsFilter>? Dimensions { get; set; }
}
