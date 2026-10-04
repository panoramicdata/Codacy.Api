namespace Codacy.Api.Models;

/// <summary>
/// Configuration status metadata
/// </summary>
public class ConfigurationStatusMetadata
{
	/// <summary>Whether the first signup is done</summary>
	public required bool FirstSignupDone { get; set; }
}
