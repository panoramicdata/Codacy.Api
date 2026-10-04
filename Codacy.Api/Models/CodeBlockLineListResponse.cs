namespace Codacy.Api.Models;

/// <summary>
/// Code block line list response
/// </summary>
public class CodeBlockLineListResponse
{
	/// <summary>Lines of the code block</summary>
	public required List<CodeBlockLine> Data { get; set; }
}
