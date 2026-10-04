using System.Text.Json.Serialization;

namespace Codacy.Api.Models;

/// <summary>
/// Payment plan model
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<PaymentPlanModel>))]
public enum PaymentPlanModel
{
	/// <summary>Auto</summary>
	[JsonStringEnumMemberName("Auto")]
	Auto,

	/// <summary>Manual</summary>
	[JsonStringEnumMemberName("Manual")]
	Manual
}
