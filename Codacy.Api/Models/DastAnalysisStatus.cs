using System.Text.Json.Serialization;

namespace Codacy.Api.Models;

/// <summary>
/// The analysis status of a DAST target
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<DastAnalysisStatus>))]
public enum DastAnalysisStatus
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
