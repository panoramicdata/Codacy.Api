using System.Text.Json.Serialization;

namespace Codacy.Api.Models;

/// <summary>
/// Result of the audit action
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<AuditResultType>))]
public enum AuditResultType
{
	/// <summary>Succeeded</summary>
	[JsonStringEnumMemberName("succeed")]
	Succeed,

	/// <summary>Failed</summary>
	[JsonStringEnumMemberName("failed")]
	Failed,

	/// <summary>Rejected</summary>
	[JsonStringEnumMemberName("rejected")]
	Rejected
}
