namespace Codacy.Api.Models;

/// <summary>
/// Summary of a Docker image
/// </summary>
public class ImageSummary
{
	/// <summary>The name of the Docker image</summary>
	public required string ImageName { get; set; }

	/// <summary>The most recently uploaded tag for this image</summary>
	public string? LatestTag { get; set; }

	/// <summary>When the last SBOM was uploaded</summary>
	public DateTimeOffset? LastSbomUploaded { get; set; }

	/// <summary>When the last SBOM was generated</summary>
	public DateTimeOffset? LastSbomGenerated { get; set; }

	/// <summary>Number of tags uploaded for this image</summary>
	public required int TagCount { get; set; }
}
