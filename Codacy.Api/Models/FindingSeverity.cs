using System.Text.Json.Serialization;

namespace Codacy.Api.Models;

/// <summary>
/// Finding severity level
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<FindingSeverity>))]
public enum FindingSeverity
{
	/// <summary>Critical</summary>
	Critical,

	/// <summary>High</summary>
	High,

	/// <summary>Medium</summary>
	Medium,

	/// <summary>Low</summary>
	Low
}
