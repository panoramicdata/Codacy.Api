using System.Reflection;
using System.Text.Json.Serialization;
using Codacy.Api.Models;
using Refit;

namespace Codacy.Api;

/// <summary>
/// Formats values Refit substitutes into URLs — path segments and query strings — the way the Codacy
/// API expects them.
/// </summary>
/// <remarks>
/// Refit's default formatter calls <see cref="object.ToString"/>, which is wrong twice here.
/// <see cref="Provider"/> renders as <c>Github</c> where the API wants <c>gh</c>: the
/// <see cref="JsonStringEnumMemberNameAttribute"/> on the enum governs JSON bodies, not URLs, so the
/// short code it declares never reached a path. Booleans render as <c>True</c> where the API wants
/// <c>true</c>, so a query string read <c>?enabled=True</c>.
/// </remarks>
public class CodacyUrlParameterFormatter : DefaultUrlParameterFormatter
{
	/// <inheritdoc />
	public override string? Format(object? value, ICustomAttributeProvider attributeProvider, Type type)
		=> value switch
		{
			// The provider short code the API addresses repositories by: gh, gl, bb.
			Enum enumValue when TryGetJsonName(enumValue, out var jsonName) => jsonName,

			// Lower case, as JSON and every other part of this API spells it.
			bool boolean => boolean ? "true" : "false",

			_ => base.Format(value, attributeProvider, type)
		};

	/// <summary>
	/// The name an enum member declares for serialisation, when it declares one.
	/// </summary>
	private static bool TryGetJsonName(Enum value, out string? jsonName)
	{
		jsonName = value
			.GetType()
			.GetField(value.ToString())
			?.GetCustomAttribute<JsonStringEnumMemberNameAttribute>()
			?.Name;

		return jsonName is not null;
	}
}
