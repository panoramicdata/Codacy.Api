namespace Codacy.Api.Models;

/// <summary>
/// List of supported file extensions for a specific language
/// </summary>
public class RepositoryLanguage : Language
{
	/// <summary>Default Codacy extensions for the language</summary>
	public required List<string> CodacyDefaults { get; set; }

	/// <summary>List of custom extensions for the language</summary>
	public required List<string> Extensions { get; set; }

	/// <summary>Default Codacy files for the language</summary>
	public required List<string> DefaultFiles { get; set; }

	/// <summary>Whether this language is analyzed for the repository</summary>
	public required bool Enabled { get; set; }

	/// <summary>Whether Codacy detected this language in the repository (requires at least one analysis)</summary>
	public required bool Detected { get; set; }
}
