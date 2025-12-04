namespace Codacy.Api.Models;

/// <summary>
/// Email notification settings (optional for updates)
/// </summary>
public class EmailNotificationSettingsOptional
{
	/// <summary>Per commit notifications</summary>
	public bool? PerCommit { get; set; }

	/// <summary>Per pull request notifications</summary>
	public bool? PerPullRequest { get; set; }

	/// <summary>Only my activity</summary>
	public bool? OnlyMyActivity { get; set; }
}
