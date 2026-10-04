namespace Codacy.Api.Models;

/// <summary>
/// Line of a code block
/// </summary>
public class CodeBlockLine
{
	/// <summary>Line number</summary>
	public required int Number { get; set; }

	/// <summary>Line content</summary>
	public required string Content { get; set; }
}
