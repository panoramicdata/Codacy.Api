namespace Codacy.Api.Models;

/// <summary>
/// Email notification settings
/// </summary>
public class EmailNotificationSettings
{
	/// <summary>Per commit notifications</summary>
	public required bool PerCommit { get; set; }

	/// <summary>Per pull request notifications</summary>
	public required bool PerPullRequest { get; set; }

	/// <summary>Only my activity</summary>
	public required bool OnlyMyActivity { get; set; }
}
