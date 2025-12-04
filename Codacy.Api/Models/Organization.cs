using System.Text.Json.Serialization;

namespace Codacy.Api.Models;

/// <summary>
/// Organization information
/// </summary>
public class Organization
{
	/// <summary>Organization identifier</summary>
	[JsonPropertyName("identifier")]
	public long? Identifier { get; set; }

	/// <summary>Remote identifier on Git provider</summary>
	[JsonPropertyName("remoteIdentifier")]
	public string? RemoteIdentifier { get; set; }

	/// <summary>Organization name</summary>
	[JsonPropertyName("name")]
	public string? Name { get; set; }

	/// <summary>Avatar URL</summary>
	[JsonPropertyName("avatar")]
	public string? Avatar { get; set; }

	/// <summary>Creation timestamp</summary>
	[JsonPropertyName("created")]
	public DateTimeOffset? Created { get; set; }

	/// <summary>Git provider</summary>
	[JsonPropertyName("provider")]
	public Provider? Provider { get; set; }

	/// <summary>Join mode</summary>
	[JsonPropertyName("joinMode")]
	public JoinMode? JoinMode { get; set; }

	/// <summary>Organization type</summary>
	[JsonPropertyName("type")]
	public OrganizationType? Type { get; set; }

	/// <summary>Join status</summary>
	[JsonPropertyName("joinStatus")]
	public JoinStatus? JoinStatus { get; set; }

	/// <summary>Single provider login</summary>
	[JsonPropertyName("singleProviderLogin")]
	public bool? SingleProviderLogin { get; set; }

	/// <summary>Has DAST access</summary>
	[JsonPropertyName("hasDastAccess")]
	public bool? HasDastAccess { get; set; }

	/// <summary>Has SCA enabled</summary>
	[JsonPropertyName("hasScaEnabled")]
	public bool? HasScaEnabled { get; set; }
}

/// <summary>
/// Join status
/// </summary>
public enum JoinStatus
{
	/// <summary>Member of organization</summary>
	member,
	/// <summary>Pending member approval</summary>
	pendingMember,
	/// <summary>Member from remote provider</summary>
	remoteMember
}

/// <summary>
/// Organization list response
/// </summary>
public class OrganizationListResponse
{
	/// <summary>Pagination info</summary>
	[JsonPropertyName("pagination")]
	public PaginationInfo? Pagination { get; set; }

	/// <summary>Organizations</summary>
	[JsonPropertyName("data")]
	public required List<Organization> Data { get; set; }
}

/// <summary>
/// Organization response
/// </summary>
public class OrganizationResponse
{
	/// <summary>Organization data</summary>
	[JsonPropertyName("data")]
	public required Organization Data { get; set; }
}
