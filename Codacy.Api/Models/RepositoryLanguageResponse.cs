namespace Codacy.Api.Models;

/// <summary>
/// Repository languages response
/// </summary>
public class RepositoryLanguageResponse
{
	/// <summary>List of languages with supported extensions</summary>
	public required List<RepositoryLanguage> Languages { get; set; }
}
