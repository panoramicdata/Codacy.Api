using System.Text.Json.Serialization;

namespace Codacy.Api.Models;

/// <summary>
/// Codacy product
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<CodacyProduct>))]
public enum CodacyProduct
{
	/// <summary>Quality</summary>
	[JsonStringEnumMemberName("quality")]
	Quality,

	/// <summary>Coverage</summary>
	[JsonStringEnumMemberName("coverage")]
	Coverage
}
