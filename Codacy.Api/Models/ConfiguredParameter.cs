namespace Codacy.Api.Models;

/// <summary>
/// Parameter to configure a code pattern for a tool
/// </summary>
public class ConfiguredParameter
{
	/// <summary>Code pattern parameter name</summary>
	public required string Name { get; set; }

	/// <summary>Code pattern parameter value</summary>
	public required string Value { get; set; }
}
