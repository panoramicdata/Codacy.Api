namespace Codacy.Api.Models;

/// <summary>
/// Settings for Git provider integrations
/// </summary>
public class RepositoryIntegrationSettings
{
	/// <summary>Integration settings</summary>
	public required ProviderIntegrationSettingsBody Settings { get; set; }

	/// <summary>Who integrated the repository</summary>
	public string? IntegratedBy { get; set; }
}
