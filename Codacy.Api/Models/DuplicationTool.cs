namespace Codacy.Api.Models;

/// <summary>
/// Codacy tool that can detect duplication on projects
/// </summary>
public class DuplicationTool
{
	/// <summary>Docker image used to launch the tool</summary>
	public required string DockerImage { get; set; }

	/// <summary>Languages that the tool supports</summary>
	public required List<string> Languages { get; set; }
}
