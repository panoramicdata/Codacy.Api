using System.Text.Json.Serialization;

namespace Codacy.Api.Models;

/// <summary>
/// Role of the audit actor
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<AuditActorRole>))]
public enum AuditActorRole
{
	/// <summary>Repository read</summary>
	[JsonStringEnumMemberName("repositoryRead")]
	RepositoryRead,

	/// <summary>Repository write</summary>
	[JsonStringEnumMemberName("repositoryWrite")]
	RepositoryWrite,

	/// <summary>Repository admin</summary>
	[JsonStringEnumMemberName("repositoryAdmin")]
	RepositoryAdmin,

	/// <summary>Organization member</summary>
	[JsonStringEnumMemberName("organizationMember")]
	OrganizationMember,

	/// <summary>Organization manager</summary>
	[JsonStringEnumMemberName("organizationManager")]
	OrganizationManager,

	/// <summary>Organization admin</summary>
	[JsonStringEnumMemberName("organizationAdmin")]
	OrganizationAdmin
}
