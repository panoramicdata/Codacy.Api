namespace Codacy.Api.Models;

/// <summary>
/// Git provider
/// </summary>
public enum Provider
{
	/// <summary>GitHub</summary>
	gh,
	/// <summary>GitLab</summary>
	gl,
	/// <summary>Bitbucket</summary>
	bb,
	/// <summary>GitHub Enterprise</summary>
	ghe,
	/// <summary>GitLab Enterprise</summary>
	gle,
	/// <summary>Bitbucket Server</summary>
	bbs
}

/// <summary>
/// Pagination information
/// </summary>
public class PaginationInfo
{
	/// <summary>Cursor for next page</summary>
	public string? Cursor { get; set; }
	/// <summary>Maximum number of items</summary>
	public int? Limit { get; set; }
	/// <summary>Total number of items</summary>
	public int? Total { get; set; }
}

/// <summary>
/// Repository visibility
/// </summary>
public enum Visibility
{
	/// <summary>Public repository</summary>
	Public,
	/// <summary>Private repository</summary>
	Private,
	/// <summary>Public for logged in users</summary>
	LoginPublic
}

/// <summary>
/// User permission level
/// </summary>
[System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1711:Identifiers should not have incorrect suffix")]
public enum Permission
{
	/// <summary>Administrator permission</summary>
	admin,
	/// <summary>Write permission</summary>
	write,
	/// <summary>Read permission</summary>
	read
}

/// <summary>
/// Issue severity level
/// </summary>
public enum SeverityLevel
{
	/// <summary>Informational severity</summary>
	Info,
	/// <summary>Warning severity</summary>
	Warning,
	/// <summary>High severity</summary>
	High,
	/// <summary>Error severity</summary>
	Error
}

/// <summary>
/// Join mode for organizations
/// </summary>
public enum JoinMode
{
	/// <summary>Automatic join</summary>
	auto,
	/// <summary>Admin automatic approval</summary>
	adminAuto,
	/// <summary>Request to join</summary>
	request
}

/// <summary>
/// Organization type
/// </summary>
public enum OrganizationType
{
	/// <summary>User account</summary>
	Account,
	/// <summary>Organization</summary>
	Organization
}

/// <summary>
/// User role
/// </summary>
public enum UserRole
{
	/// <summary>Administrator role</summary>
	admin,
	/// <summary>Manager role</summary>
	manager,
	/// <summary>Member role</summary>
	member
}

/// <summary>
/// User response
/// </summary>
public class UserResponse
{
	/// <summary>User data</summary>
	public required User Data { get; set; }
}

/// <summary>
/// User information
/// </summary>
public class User
{
	/// <summary>User ID</summary>
	public required long Id { get; set; }

	/// <summary>Name</summary>
	public string? Name { get; set; }

	/// <summary>Main email</summary>
	public required string MainEmail { get; set; }

	/// <summary>Other emails</summary>
	public required List<string> OtherEmails { get; set; }

	/// <summary>Is admin</summary>
	public required bool IsAdmin { get; set; }

	/// <summary>Is active</summary>
	public required bool IsActive { get; set; }

	/// <summary>Created timestamp</summary>
	public required DateTimeOffset Created { get; set; }

	/// <summary>Intercom hash</summary>
	public string? IntercomHash { get; set; }

	/// <summary>Zendesk hash</summary>
	public string? ZendeskHash { get; set; }

	/// <summary>Should do client qualification</summary>
	public bool? ShouldDoClientQualification { get; set; }
}

/// <summary>
/// User body for updates
/// </summary>
public class UserBody
{
	/// <summary>Name</summary>
	public string? Name { get; set; }

	/// <summary>Should do client qualification</summary>
	public bool? ShouldDoClientQualification { get; set; }
}

/// <summary>
/// API token list response
/// </summary>
public class ApiTokenListResponse
{
	/// <summary>Pagination info</summary>
	public PaginationInfo? Pagination { get; set; }

	/// <summary>API tokens</summary>
	public required List<ApiToken> Data { get; set; }
}

/// <summary>
/// API token
/// </summary>
public class ApiToken
{
	/// <summary>Token ID</summary>
	public required long Id { get; set; }

	/// <summary>Token value</summary>
	public required string Token { get; set; }

	/// <summary>Expiration date</summary>
	public DateTimeOffset? ExpiresAt { get; set; }
}

/// <summary>
/// API token create request
/// </summary>
public class ApiTokenCreateRequest
{
	/// <summary>Expiration date</summary>
	public DateTimeOffset? ExpiresAt { get; set; }
}

/// <summary>
/// User email
/// </summary>
public class UserEmail
{
	/// <summary>Email address</summary>
	public required string Email { get; set; }

	/// <summary>Is private</summary>
	public required bool IsPrivate { get; set; }
}

/// <summary>
/// User emails
/// </summary>
public class UserEmails
{
	/// <summary>Main email</summary>
	public required UserEmail MainEmail { get; set; }

	/// <summary>Other emails</summary>
	public required List<UserEmail> OtherEmails { get; set; }
}

/// <summary>
/// User emails response
/// </summary>
public class UserEmailsResponse
{
	/// <summary>Email data</summary>
	public required UserEmails Data { get; set; }
}

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

/// <summary>
/// Email notification settings response
/// </summary>
public class EmailNotificationSettingsResponse
{
	/// <summary>Settings data</summary>
	public required EmailNotificationSettings Data { get; set; }
}

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

/// <summary>
/// Integration
/// </summary>
public class Integration
{
	/// <summary>Provider</summary>
	public required Provider Provider { get; set; }

	/// <summary>Host</summary>
	public required string Host { get; set; }

	/// <summary>Last authenticated</summary>
	public required DateTimeOffset LastAuthenticated { get; set; }
}

/// <summary>
/// Integration list response
/// </summary>
public class IntegrationListResponse
{
	/// <summary>Pagination info</summary>
	public PaginationInfo? Pagination { get; set; }

	/// <summary>Integrations</summary>
	public required List<Integration> Data { get; set; }
}
