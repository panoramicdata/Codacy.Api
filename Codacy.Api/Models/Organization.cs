namespace Codacy.Api.Models;

/// <summary>
/// Organization information
/// </summary>
public class Organization
{
	/// <summary>Organization identifier</summary>
	public long? Identifier { get; set; }

	/// <summary>Remote identifier on Git provider</summary>
	public string? RemoteIdentifier { get; set; }

	/// <summary>Organization name</summary>
	public string? Name { get; set; }

	/// <summary>Avatar URL</summary>
	public string? Avatar { get; set; }

	/// <summary>Creation timestamp</summary>
	public DateTimeOffset? Created { get; set; }

	/// <summary>Git provider</summary>
	public Provider? Provider { get; set; }

	/// <summary>Join mode</summary>
	public JoinMode? JoinMode { get; set; }

	/// <summary>Organization type</summary>
	public OrganizationType? Type { get; set; }

	/// <summary>Join status</summary>
	public JoinStatus? JoinStatus { get; set; }

	/// <summary>Single provider login</summary>
	public bool? SingleProviderLogin { get; set; }

	/// <summary>Has DAST access</summary>
	public bool? HasDastAccess { get; set; }

	/// <summary>Has SCA enabled</summary>
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
	public PaginationInfo? Pagination { get; set; }

	/// <summary>Organizations</summary>
	public required List<Organization> Data { get; set; }
}

/// <summary>
/// Organization response
/// </summary>
public class OrganizationResponse
{
	/// <summary>Organization data</summary>
	public required Organization Data { get; set; }
}
