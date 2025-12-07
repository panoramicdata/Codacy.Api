namespace Codacy.Api.Models;

/// <summary>
/// Patterns count
/// </summary>
public class PatternsCount
{
	/// <summary>Pattern ID</summary>
	public required string Id { get; set; }

	/// <summary>Pattern title</summary>
	public required string Title { get; set; }

	/// <summary>Total count</summary>
	public required int Total { get; set; }
}
