namespace Codacy.Api.Models;

/// <summary>
/// Response containing a single tool pattern
/// </summary>
public class PatternResponse
{
	/// <summary>Pattern</summary>
	public required Pattern Data { get; set; }
}
