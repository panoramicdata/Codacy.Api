namespace Codacy.Api.Models;

/// <summary>
/// List of languages supported by available tools
/// </summary>
public class LanguageListResponse
{
	/// <summary>Languages</summary>
	public required List<LanguageFileExtension> Data { get; set; }
}
