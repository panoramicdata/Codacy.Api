namespace Codacy.Api.Models;

/// <summary>
/// Restricts a metric query to a dimension value
/// </summary>
public class DimensionsFilter
{
	/// <summary>Dimension name</summary>
	public required string Dimension { get; set; }

	/// <summary>Dimension value</summary>
	public required string Value { get; set; }
}
