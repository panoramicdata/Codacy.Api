namespace Codacy.Api.Models;

/// <summary>
/// A target for DAST analysis
/// </summary>
public class DastTarget
{
	/// <summary>Target identifier</summary>
	public required long Id { get; set; }

	/// <summary>Target URL</summary>
	public required string Url { get; set; }

	/// <summary>The analysis statuses the target is in. Can be empty if the target was never analysed.</summary>
	public required List<DastTargetStatus> Status { get; set; }

	/// <summary>Target type</summary>
	public DastTargetType? TargetType { get; set; }

	/// <summary>API definition URL</summary>
	public string? ApiDefinitionUrl { get; set; }

	/// <summary>Names of the API authentication headers</summary>
	public List<string>? ApiAuthHeaderNames { get; set; }
}
