namespace Codacy.Api.Models;

/// <summary>
/// SSH key setting response
/// </summary>
public class SshKeySettingResponse
{
	/// <summary>Public SSH key</summary>
	public required string PublicSshKey { get; set; }
}
