using System.Text.Json.Serialization;

namespace Codacy.Api.Models;

/// <summary>
/// Autoconfig analysis status
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<AutoconfigStatus>))]
public enum AutoconfigStatus
{
	/// <summary>Queued</summary>
	[JsonStringEnumMemberName("queued")]
	Queued,

	/// <summary>Running</summary>
	[JsonStringEnumMemberName("running")]
	Running,

	/// <summary>Successful</summary>
	[JsonStringEnumMemberName("successful")]
	Successful,

	/// <summary>Failed</summary>
	[JsonStringEnumMemberName("failed")]
	Failed
}
