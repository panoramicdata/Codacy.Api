using System.Text.Json.Serialization;

namespace Codacy.Api.Models;

/// <summary>
/// Type of Codacy element
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<ElementType>))]
public enum ElementType
{
	/// <summary>Issue</summary>
	[JsonStringEnumMemberName("issue")]
	Issue,

	/// <summary>Finding</summary>
	[JsonStringEnumMemberName("finding")]
	Finding,

	/// <summary>File</summary>
	[JsonStringEnumMemberName("file")]
	File,

	/// <summary>Dependency</summary>
	[JsonStringEnumMemberName("dependency")]
	Dependency
}
