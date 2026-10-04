namespace Codacy.Api.Models;

/// <summary>
/// List of gate policies for an organization
/// </summary>
public class GatePoliciesListResponse
{
	/// <summary>The gate policies</summary>
	public required List<GatePolicySummarized> Data { get; set; }
}
