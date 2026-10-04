namespace Codacy.Api.Models;

/// <summary>
/// Configured login provider
/// </summary>
public class ConfiguredLoginIntegration
{
	/// <summary>Git provider</summary>
	public required Provider Provider { get; set; }

	/// <summary>Login URL</summary>
	public required string LoginUrl { get; set; }
}
