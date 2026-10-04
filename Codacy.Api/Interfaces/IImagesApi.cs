using Codacy.Api.Models;
using Refit;

namespace Codacy.Api.Interfaces;

/// <summary>
/// Interface for Docker image SBOM API operations
/// </summary>
public interface IImagesApi
{
	/// <summary>
	/// Upload an SBOM (SPDX or CycloneDX) for a Docker image
	/// </summary>
	[Multipart]
	[Post("/api/v3/organizations/{provider}/{organizationName}/image-sboms")]
	Task UploadImageSbomAsync(
		Provider provider,
		string organizationName,
		[AliasAs("sbom")] StreamPart sbom,
		[AliasAs("imageName")] string imageName,
		[AliasAs("tag")] string tag,
		[AliasAs("repositoryName")] string? repositoryName,
		[AliasAs("environment")] string? environment,
		CancellationToken cancellationToken);

	/// <summary>
	/// Delete all SBOMs for a given image
	/// </summary>
	[Delete("/api/v3/organizations/{provider}/{organizationName}/image-sboms/{imageName}")]
	Task DeleteImageSbomsAsync(
		Provider provider,
		string organizationName,
		string imageName,
		CancellationToken cancellationToken);

	/// <summary>
	/// Delete the SBOM for a given image and tag
	/// </summary>
	[Delete("/api/v3/organizations/{provider}/{organizationName}/image-sboms/{imageName}/tags/{tag}")]
	Task DeleteImageTagAsync(
		Provider provider,
		string organizationName,
		string imageName,
		string tag,
		CancellationToken cancellationToken);

	/// <summary>
	/// List Docker images for an organization
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/images")]
	Task<ListImagesResponse> ListOrganizationImagesAsync(
		Provider provider,
		string organizationName,
		[Query] string? cursor,
		[Query] int? limit,
		CancellationToken cancellationToken);

	/// <summary>
	/// List the tags of a Docker image
	/// </summary>
	[Get("/api/v3/organizations/{provider}/{organizationName}/images/{imageName}/tags")]
	Task<ListResponse<ImageTagSummary>> ListImageTagsAsync(
		Provider provider,
		string organizationName,
		string imageName,
		[Query] string? cursor,
		[Query] int? limit,
		CancellationToken cancellationToken);
}
