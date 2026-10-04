namespace Codacy.Api.Models;

/// <summary>
/// Codacy tool that can flag patterns/issues on projects
/// </summary>
public class Tool : ToolReference
{
	/// <summary>Original tool version used by the Codacy tool wrapper</summary>
	public required string Version { get; set; }

	/// <summary>Tool unique short name, containing alphanumeric characters only and no spaces</summary>
	public required string ShortName { get; set; }

	/// <summary>Original tool documentation URL</summary>
	public string? DocumentationUrl { get; set; }

	/// <summary>Codacy tool wrapper source code URL</summary>
	public string? SourceCodeUrl { get; set; }

	/// <summary>Tool prefix used to ensure pattern names are unique</summary>
	public string? Prefix { get; set; }

	/// <summary>Tool requires compilation to run</summary>
	public required bool NeedsCompilation { get; set; }

	/// <summary>Tool configuration filenames</summary>
	public required List<string> ConfigurationFilenames { get; set; }

	/// <summary>Tool description</summary>
	public string? Description { get; set; }

	/// <summary>Docker image used to launch the tool</summary>
	public required string DockerImage { get; set; }

	/// <summary>Languages that the tool supports</summary>
	public required List<string> Languages { get; set; }

	/// <summary>True if the tool is supposed to run on the client machine and the results sent to Codacy</summary>
	public required bool ClientSide { get; set; }

	/// <summary>True if the client-side tool runs stand-alone outside of the CLI</summary>
	public required bool Standalone { get; set; }

	/// <summary>True if the tool is enabled by default for new projects</summary>
	public required bool EnabledByDefault { get; set; }

	/// <summary>True if the tool is configurable on the Codacy UI</summary>
	public required bool Configurable { get; set; }
}
