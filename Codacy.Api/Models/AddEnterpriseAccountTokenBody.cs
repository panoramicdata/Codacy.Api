namespace Codacy.Api.Models;

/// <summary>
/// Details of a new enterprise account token
/// </summary>
public class AddEnterpriseAccountTokenBody
{
	/// <summary>Token</summary>
	public required string Token { get; set; }

	/// <summary>Git provider</summary>
	public required Provider Provider { get; set; }
}
