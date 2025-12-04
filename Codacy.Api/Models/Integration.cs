namespace Codacy.Api.Models;

/// <summary>
/// Integration
/// </summary>
public class Integration
{
	/// <summary>Provider</summary>
	public required Provider Provider { get; set; }

	/// <summary>Host</summary>
	public required string Host { get; set; }

	/// <summary>Last authenticated</summary>
	public required DateTimeOffset LastAuthenticated { get; set; }
}
