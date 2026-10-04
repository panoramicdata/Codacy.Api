namespace Codacy.Api.Models;

/// <summary>
/// Code pattern that a Codacy tool can use to find issues
/// </summary>
public class Pattern : PatternDetails
{
	/// <summary>Short description of the code pattern</summary>
	public string? Description { get; set; }

	/// <summary>Full description of the code pattern, in CommonMark</summary>
	public string? Explanation { get; set; }

	/// <summary>True if the code pattern is on by default for new repositories</summary>
	public required bool Enabled { get; set; }

	/// <summary>Languages that the code pattern supports</summary>
	public List<string>? Languages { get; set; }

	/// <summary>Average time to fix an issue detected by the code pattern, in minutes</summary>
	public int? TimeToFix { get; set; }

	/// <summary>Parameters of the code pattern</summary>
	public required List<PatternParameter> Parameters { get; set; }

	/// <summary>Rationale for the pattern</summary>
	public string? Rationale { get; set; }

	/// <summary>Suggested solution for the pattern</summary>
	public string? Solution { get; set; }

	/// <summary>Good examples for the pattern</summary>
	public List<string>? GoodExamples { get; set; }

	/// <summary>Bad examples for the pattern</summary>
	public List<string>? BadExamples { get; set; }

	/// <summary>Tags associated with the pattern</summary>
	public List<string>? Tags { get; set; }
}
