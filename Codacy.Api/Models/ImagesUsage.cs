namespace Codacy.Api.Models;

/// <summary>
/// Image tag usage against the organization limit
/// </summary>
public class ImagesUsage
{
	/// <summary>Number of image tags uploaded across the organization, counted against the limit</summary>
	public required int ImageTags { get; set; }

	/// <summary>Maximum number of image tags allowed for the organization</summary>
	public required int Limit { get; set; }
}
