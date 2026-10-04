using System.Text.Json.Serialization;

namespace Codacy.Api.Models;

/// <summary>
/// Outcome of an analysis reported by a webhook delivery
/// </summary>
public enum WebhookAnalysisStatus
{
	/// <summary>The analysis succeeded</summary>
	[JsonStringEnumMemberName("success")]
	Success,

	/// <summary>The analysis completed only in part</summary>
	[JsonStringEnumMemberName("partial_success")]
	PartialSuccess,

	/// <summary>The analysis failed</summary>
	[JsonStringEnumMemberName("failure")]
	Failure
}
