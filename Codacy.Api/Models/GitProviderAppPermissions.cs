namespace Codacy.Api.Models;

/// <summary>
/// Information about Codacy GitHub App repository permissions
/// </summary>
public class GitProviderAppPermissions
{
	/// <summary>True if the app has repository Contents permissions</summary>
	public required bool ContentPermission { get; set; }

	/// <summary>True if the app has custom properties permissions</summary>
	public required bool CustomPropertiesPermission { get; set; }
}
