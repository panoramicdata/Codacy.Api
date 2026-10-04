namespace Codacy.Api.Models;

/// <summary>
/// A path recommended to be added to the repository ignore list
/// </summary>
public class AutoconfigRecommendedPath
{
	/// <summary>The path to ignore</summary>
	public required string Path { get; set; }

	/// <summary>Reason the path is recommended for ignoring</summary>
	public required string Reason { get; set; }
}
