using System.Text.Json.Serialization;

namespace Codacy.Api.Models;

/// <summary>
/// Time granularity of metric values
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<MetricPeriod>))]
public enum MetricPeriod
{
	/// <summary>Daily</summary>
	[JsonStringEnumMemberName("day")]
	Day,

	/// <summary>Weekly</summary>
	[JsonStringEnumMemberName("week")]
	Week,

	/// <summary>Monthly</summary>
	[JsonStringEnumMemberName("month")]
	Month
}
