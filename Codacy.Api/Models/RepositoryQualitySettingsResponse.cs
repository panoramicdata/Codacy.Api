namespace Codacy.Api.Models;

/// <summary>
/// Repository quality settings response
/// </summary>
public class RepositoryQualitySettingsResponse
{
	/// <summary>Settings data</summary>
	public required RepositoryQualitySettings Data { get; set; }
}
