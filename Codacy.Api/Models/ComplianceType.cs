using System.Text.Json.Serialization;

namespace Codacy.Api.Models;

/// <summary>
/// The type of compliance standard
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<ComplianceType>))]
public enum ComplianceType
{
	/// <summary>AI risk</summary>
	[JsonStringEnumMemberName("ai-risk")]
	AiRisk
}
