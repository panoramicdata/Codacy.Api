namespace Codacy.Api.Models;

/// <summary>
/// A language supported by Codacy tools and its file extensions
/// </summary>
public class LanguageFileExtension
{
	/// <summary>The language name</summary>
	public required string Name { get; set; }

	/// <summary>The default file extensions for this language</summary>
	public required List<string> FileExtensions { get; set; }

	/// <summary>Specific files that should be considered for this language</summary>
	public required List<string> Files { get; set; }
}
