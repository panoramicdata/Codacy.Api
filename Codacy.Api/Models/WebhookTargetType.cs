namespace Codacy.Api.Models;

/// <summary>
/// What a webhook delivery's analysis ran against
/// </summary>
public enum WebhookTargetType
{
	/// <summary>A branch</summary>
	Branch,

	/// <summary>A pull request</summary>
	PullRequest
}
