namespace Codacy.Api.Models;

/// <summary>
/// Whether a repository has quick fix suggestions
/// </summary>
public class HasQuickfixSuggestionsResponse
{
	/// <summary>True if there are quick fix suggestions</summary>
	public required bool HasSuggestions { get; set; }
}
