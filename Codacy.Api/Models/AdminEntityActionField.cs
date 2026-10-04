namespace Codacy.Api.Models;

/// <summary>
/// Field of an admin entity action
/// </summary>
public class AdminEntityActionField
{
	/// <summary>Field identifier used in the payload</summary>
	public required string Name { get; set; }

	/// <summary>Human-friendly name for the field</summary>
	public required string DisplayName { get; set; }

	/// <summary>Description of what the field controls</summary>
	public string? Description { get; set; }

	/// <summary>Whether the field is required</summary>
	public required bool Required { get; set; }

	/// <summary>The type of the field (boolean, number, string)</summary>
	public required string FieldType { get; set; }

	/// <summary>Current value if fieldType is boolean</summary>
	public bool? CurrentValueBoolean { get; set; }

	/// <summary>Current value if fieldType is number</summary>
	public long? CurrentValueNumber { get; set; }

	/// <summary>Current value if fieldType is string</summary>
	public string? CurrentValueString { get; set; }

	/// <summary>Additional metadata for the field</summary>
	public Dictionary<string, string>? Metadata { get; set; }
}
