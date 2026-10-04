namespace Codacy.Api.Models;

/// <summary>
/// Group that a metric value belongs to
/// </summary>
public class MetricGroup
{
	/// <summary>Organization</summary>
	public string? Organization { get; set; }

	/// <summary>Repository</summary>
	public string? Repository { get; set; }

	/// <summary>Dimension values</summary>
	public List<string>? Dimensions { get; set; }
}
