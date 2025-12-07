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
/// Organization response wrapper containing the full organization details
/// </summary>
public class OrganizationResponse
{
	/// <summary>Organization data wrapper</summary>
	[JsonPropertyName("data")]
	public required OrganizationData Data { get; set; }
}

/// <summary>
/// Organization data containing organization details and related information
/// </summary>
public class OrganizationData
{
	/// <summary>The organization details (nested structure)</summary>
	[JsonPropertyName("organization")]
	public Organization? Organization { get; set; }

	// Properties that can be directly deserialized from flat API response
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

	// Additional OrganizationData properties
	/// <summary>User membership information for this organization</summary>
	[JsonPropertyName("membership")]
	public OrganizationMembership? Membership { get; set; }

	/// <summary>Minimum permission required for analysis configuration</summary>
	[JsonPropertyName("analysisConfigurationMinimumPermission")]
	public string? AnalysisConfigurationMinimumPermission { get; set; }

	/// <summary>Subscriptions for this organization</summary>
	[JsonPropertyName("subscriptions")]
	public List<OrganizationSubscription>? Subscriptions { get; set; }

	/// <summary>Billing information</summary>
	[JsonPropertyName("billing")]
	public OrganizationBilling? Billing { get; set; }

	/// <summary>Paywall status</summary>
	[JsonPropertyName("paywall")]
	public PaywallStatus? Paywall { get; set; }

	/// <summary>Organization-level paywall</summary>
	[JsonPropertyName("organizationPayWall")]
	public OrganizationPaywall? OrganizationPayWall { get; set; }
}

/// <summary>
/// User membership information for an organization
/// </summary>
public class OrganizationMembership
{
	/// <summary>User's role in the organization</summary>
	[JsonPropertyName("userRole")]
	public string? UserRole { get; set; }
}

/// <summary>
/// Organization subscription information
/// </summary>
public class OrganizationSubscription
{
	/// <summary>Product name</summary>
	[JsonPropertyName("product")]
	public string? Product { get; set; }

	/// <summary>Subscription plan details</summary>
	[JsonPropertyName("plan")]
	public SubscriptionPlan? Plan { get; set; }

	/// <summary>Subscription paywall status</summary>
	[JsonPropertyName("paywall")]
	public PaywallStatus? Paywall { get; set; }
}

/// <summary>
/// Subscription plan details
/// </summary>
public class SubscriptionPlan
{
	/// <summary>Whether this is a premium plan</summary>
	[JsonPropertyName("isPremium")]
	public bool? IsPremium { get; set; }

	/// <summary>Plan code</summary>
	[JsonPropertyName("code")]
	public string? Code { get; set; }
}

/// <summary>
/// Organization billing information
/// </summary>
public class OrganizationBilling
{
	/// <summary>Whether this is a premium subscription</summary>
	[JsonPropertyName("isPremium")]
	public bool? IsPremium { get; set; }

	/// <summary>Billing model</summary>
	[JsonPropertyName("model")]
	public string? Model { get; set; }

	/// <summary>Billing code</summary>
	[JsonPropertyName("code")]
	public string? Code { get; set; }
}

/// <summary>
/// Paywall status
/// </summary>
public class PaywallStatus
{
	/// <summary>Whether organization dashboard is paywalled</summary>
	[JsonPropertyName("organizationDashboard")]
	public bool? OrganizationDashboard { get; set; }

	/// <summary>Whether security dashboard is paywalled</summary>
	[JsonPropertyName("securityDashboard")]
	public bool? SecurityDashboard { get; set; }
}

/// <summary>
/// Organization-level paywall
/// </summary>
public class OrganizationPaywall
{
	/// <summary>Whether organization dashboard is paywalled</summary>
	[JsonPropertyName("organizationDashboard")]
	public bool? OrganizationDashboard { get; set; }
}
