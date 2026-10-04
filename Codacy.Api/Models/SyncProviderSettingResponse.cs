namespace Codacy.Api.Models;

/// <summary>
/// Repository name and visibility synchronized with the Git provider
/// </summary>
public class SyncProviderSettingResponse
{
	/// <summary>Name of the repository</summary>
	public required string Name { get; set; }

	/// <summary>Visibility of the repository</summary>
	public required Visibility Visibility { get; set; }
}
