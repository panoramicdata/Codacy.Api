namespace Codacy.Api.Models;

/// <summary>
/// How to group metric values
/// </summary>
public class MetricGroupBy
{
	/// <summary>Values can be organization, repository, or a dimension type that depends on the metric</summary>
	public required List<string> GroupBy { get; set; }

	/// <summary>Sort direction</summary>
	public string? SortDirection { get; set; }

	/// <summary>Maximum number of groups</summary>
	public int? Limit { get; set; }
}
