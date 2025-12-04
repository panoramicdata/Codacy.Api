namespace Codacy.Api.Models;

/// <summary>
/// Git provider
/// </summary>
public enum Provider
{
	/// <summary>
	/// GitHub
	/// </summary>
	gh,

	/// <summary>
	/// GitLab
	/// </summary>
	gl,

	/// <summary>
	/// Bitbucket
	/// </summary>
	bb,

	/// <summary>
	/// GitHub Enterprise
	/// </summary>
	ghe,

	/// <summary>GitLab Enterprise</summary>
	gle,
	/// <summary>Bitbucket Server</summary>
	bbs
}
