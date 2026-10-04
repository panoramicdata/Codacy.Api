namespace Codacy.Api.Models;

/// <summary>
/// Update of the settings of a language
/// </summary>
public class RepositoryLanguageUpdate : Language
{
	/// <summary>Custom file extensions for the language; if left undefined the extensions are not updated</summary>
	public List<string>? Extensions { get; set; }

	/// <summary>Whether this language is analyzed for the repository; if left undefined the flag is not updated</summary>
	public bool? Enabled { get; set; }
}
