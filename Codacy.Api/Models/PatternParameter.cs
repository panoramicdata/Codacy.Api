namespace Codacy.Api.Models;

/// <summary>
/// Parameter to configure a code pattern
/// </summary>
public class PatternParameter
{
	/// <summary>Name of the parameter</summary>
	public required string Name { get; set; }

	/// <summary>Description of the parameter</summary>
	public string? Description { get; set; }

	/// <summary>Default value of the parameter</summary>
	public required string Default { get; set; }
}
