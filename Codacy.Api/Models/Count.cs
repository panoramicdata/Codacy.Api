namespace Codacy.Api.Models;

/// <summary>
/// Count
/// </summary>
public class Count
{
	/// <summary>Name</summary>
	public required string Name { get; set; }

	/// <summary>Total count</summary>
	public required int Total { get; set; }
}
