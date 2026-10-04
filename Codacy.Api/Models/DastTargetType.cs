using System.Text.Json.Serialization;

namespace Codacy.Api.Models;

/// <summary>
/// DAST target type
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<DastTargetType>))]
public enum DastTargetType
{
	/// <summary>Web application</summary>
	[JsonStringEnumMemberName("webapp")]
	WebApp,

	/// <summary>OpenAPI definition</summary>
	[JsonStringEnumMemberName("openapi")]
	OpenApi,

	/// <summary>GraphQL</summary>
	[JsonStringEnumMemberName("graphql")]
	GraphQl
}
