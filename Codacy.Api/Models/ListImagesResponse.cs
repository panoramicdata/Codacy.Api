namespace Codacy.Api.Models;

/// <summary>
/// Response listing Docker images for an organization
/// </summary>
public class ListImagesResponse
{
	/// <summary>Pagination info</summary>
	public required PaginationInfo Pagination { get; set; }

	/// <summary>The images</summary>
	public required List<ImageSummary> Data { get; set; }

	/// <summary>Image tag usage</summary>
	public required ImagesUsage Usage { get; set; }
}
