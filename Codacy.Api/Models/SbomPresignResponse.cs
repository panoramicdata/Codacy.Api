namespace Codacy.Api.Models;

/// <summary>
/// Response containing a presigned URL to download the repository SBOM
/// </summary>
public class SbomPresignResponse
{
	/// <summary>Presigned S3 URL to download the SBOM JSON</summary>
	public required Uri Url { get; set; }
}
