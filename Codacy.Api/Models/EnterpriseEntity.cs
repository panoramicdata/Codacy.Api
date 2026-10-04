namespace Codacy.Api.Models;

/// <summary>
/// Enterprise
/// </summary>
public class EnterpriseEntity
{
	/// <summary>Enterprise name, used as stable enterprise identifier</summary>
	public required string Name { get; set; }

	/// <summary>Enterprise remote name, not the enterprise identifier</summary>
	public required string DisplayName { get; set; }

	/// <summary>User role in the enterprise</summary>
	public required EnterpriseUserRole UserRole { get; set; }

	/// <summary>Enterprise URL</summary>
	public required string Url { get; set; }

	/// <summary>Enterprise avatar URL</summary>
	public required string AvatarUrl { get; set; }

	/// <summary>Git provider</summary>
	public required Provider Provider { get; set; }
}
