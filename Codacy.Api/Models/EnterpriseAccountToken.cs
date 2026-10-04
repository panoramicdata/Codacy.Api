namespace Codacy.Api.Models;

/// <summary>
/// Enterprise account token
/// </summary>
public class EnterpriseAccountToken
{
	/// <summary>Git provider</summary>
	public required Provider Provider { get; set; }
}
