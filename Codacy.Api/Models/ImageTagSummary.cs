namespace Codacy.Api.Models;

/// <summary>
/// Summary of a Docker image tag
/// </summary>
public class ImageTagSummary
{
	/// <summary>The name of the Docker image</summary>
	public required string ImageName { get; set; }

	/// <summary>Tag of the Docker image</summary>
	public required string Tag { get; set; }

	/// <summary>Environment where the image is deployed</summary>
	public string? Environment { get; set; }

	/// <summary>The repository id associated with this image tag</summary>
	public long? RepositoryId { get; set; }

	/// <summary>The repository name associated with this image tag</summary>
	public string? RepositoryName { get; set; }

	/// <summary>When the SBOM for this image tag was generated</summary>
	public required DateTimeOffset GeneratedAt { get; set; }

	/// <summary>When the SBOM was uploaded</summary>
	public required DateTimeOffset UploadedAt { get; set; }

	/// <summary>Deprecated upstream, use LastAnalysedAt instead</summary>
	public DateTimeOffset? ScanStatus { get; set; }

	/// <summary>When this image tag was last analysed</summary>
	public DateTimeOffset? LastAnalysedAt { get; set; }
}
