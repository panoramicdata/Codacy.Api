namespace Codacy.Api.Models;

/// <summary>
/// A parameter value that changed as part of an autoconfig pattern update
/// </summary>
public class AutoconfigParameterChange
{
	/// <summary>Parameter identifier</summary>
	public required string Id { get; set; }

	/// <summary>Value before the change</summary>
	public required string Before { get; set; }

	/// <summary>Value after the change</summary>
	public required string After { get; set; }
}
