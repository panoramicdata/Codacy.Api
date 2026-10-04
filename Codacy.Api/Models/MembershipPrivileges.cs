using System.Text.Json.Serialization;

namespace Codacy.Api.Models;

/// <summary>
/// Minimum permission level for organization members
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<MembershipPrivileges>))]
public enum MembershipPrivileges
{
	/// <summary>Repository admin</summary>
	[JsonStringEnumMemberName("RepoAdmin")]
	RepoAdmin,

	/// <summary>Repository write</summary>
	[JsonStringEnumMemberName("RepoWrite")]
	RepoWrite,

	/// <summary>Repository read</summary>
	[JsonStringEnumMemberName("RepoRead")]
	RepoRead
}
