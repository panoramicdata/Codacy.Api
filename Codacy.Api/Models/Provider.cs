using System.Text.Json.Serialization;

namespace Codacy.Api.Models;

/// <summary>
/// Git provider
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<Provider>))]
public enum Provider
{
	/// <summary>
	/// GitHub
	/// </summary>
	[JsonStringEnumMemberName("gh")]
	Github,

	/// <summary>
	/// GitLab
	/// </summary>
	[JsonStringEnumMemberName("gl")]
	Gitlab,

	/// <summary>
	/// Bitbucket
	/// </summary>
	[JsonStringEnumMemberName("bb")]
	Bitbucket,

	/// <summary>
	/// GitHub Enterprise
	/// </summary>
	[JsonStringEnumMemberName("ghe")]
	GithubEnterprise,

	/// <summary>
	/// GitLab Enterprise
	/// </summary>
	[JsonStringEnumMemberName("gle")]
	GitlabEnterprise,

	/// <summary>Bitbucket Server</summary>
	[JsonStringEnumMemberName("bbs")]
	BitbucketServer
}
