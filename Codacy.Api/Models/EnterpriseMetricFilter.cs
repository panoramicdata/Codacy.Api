using System.Text.Json.Serialization;

namespace Codacy.Api.Models;

/// <summary>
/// Filter for an enterprise metric query
/// </summary>
/// <remarks>
/// The spec lists an enterpriseEntityFilter as required but does not define it, so it is not modelled.
/// </remarks>
public class EnterpriseMetricFilter
{
	/// <summary>Dimension filters</summary>
	[JsonPropertyName("dimensionsFilter")]
	public List<DimensionsFilter>? Dimensions { get; set; }
}
