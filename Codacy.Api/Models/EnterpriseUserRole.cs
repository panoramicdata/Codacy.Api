using System.Text.Json.Serialization;

namespace Codacy.Api.Models;

/// <summary>
/// User role in an enterprise or enterprise organization
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<EnterpriseUserRole>))]
public enum EnterpriseUserRole
{
	/// <summary>Admin</summary>
	[JsonStringEnumMemberName("admin")]
	Admin,

	/// <summary>Member</summary>
	[JsonStringEnumMemberName("member")]
	Member
}
