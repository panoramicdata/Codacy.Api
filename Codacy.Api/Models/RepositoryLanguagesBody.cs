namespace Codacy.Api.Models;

/// <summary>
/// Request body to configure the languages of a repository
/// </summary>
public class RepositoryLanguagesBody
{
	/// <summary>List of languages for this repository</summary>
	public required List<RepositoryLanguageUpdate> Languages { get; set; }
}
