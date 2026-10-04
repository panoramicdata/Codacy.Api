using System.Text.Json.Serialization;

namespace Codacy.Api.Models;

/// <summary>
/// The risk category of a license
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<LicenseRiskCategory>))]
public enum LicenseRiskCategory
{
	/// <summary>Forbidden</summary>
	Forbidden,

	/// <summary>Restricted</summary>
	Restricted,

	/// <summary>Reciprocal</summary>
	Reciprocal,

	/// <summary>Notice</summary>
	Notice,

	/// <summary>Permissive</summary>
	Permissive,

	/// <summary>Unencumbered</summary>
	Unencumbered,

	/// <summary>Unknown</summary>
	Unknown
}
