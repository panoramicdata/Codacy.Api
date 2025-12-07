namespace Codacy.Api.Models;

/// <summary>
/// File metrics
/// </summary>
public class FileMetrics
{
	/// <summary>Lines of code</summary>
	public int? LinesOfCode { get; set; }

	/// <summary>Commented lines of code</summary>
	public long? CommentedLinesOfCode { get; set; }

	/// <summary>Number of methods</summary>
	public int? NumberOfMethods { get; set; }

	/// <summary>Number of classes</summary>
	public int? NumberOfClasses { get; set; }
}
