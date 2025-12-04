namespace Codacy.Api.Models;

/// <summary>
/// Email notification settings response
/// </summary>
public class EmailNotificationSettingsResponse
{
	/// <summary>Settings data</summary>
	public required EmailNotificationSettings Data { get; set; }
}
