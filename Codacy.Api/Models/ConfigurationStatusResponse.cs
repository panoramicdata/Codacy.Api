namespace Codacy.Api.Models;

/// <summary>
/// Configuration status response
/// </summary>
public class ConfigurationStatusResponse
{
	/// <summary>Statuses</summary>
	public List<string>? Statuses { get; set; }

	/// <summary>Metadata</summary>
	public ConfigurationStatusMetadata? Metadata { get; set; }
}
