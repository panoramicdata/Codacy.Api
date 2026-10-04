namespace Codacy.Api.Models;

/// <summary>
/// Body to create a DAST target
/// </summary>
public class CreateDastTargetBody
{
	/// <summary>Target URL</summary>
	public required string Url { get; set; }

	/// <summary>Target type</summary>
	public DastTargetType? TargetType { get; set; }

	/// <summary>API definition URL</summary>
	public string? ApiDefinitionUrl { get; set; }

	/// <summary>API authentication headers, by header name</summary>
	public Dictionary<string, string>? ApiAuthHeaders { get; set; }
}
