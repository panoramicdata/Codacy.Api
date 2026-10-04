namespace Codacy.Api.Models;

/// <summary>
/// Request body for searching dependencies
/// </summary>
public class SearchSbomDependenciesBody
{
	/// <summary>Text search query matching SBOM component fields (purl, full_name)</summary>
	public string? Text { get; set; }

	/// <summary>Repository names to filter by</summary>
	public List<string>? Repositories { get; set; }

	/// <summary>Segment ids to filter by</summary>
	public List<long>? Segments { get; set; }

	/// <summary>Finding severities to filter by</summary>
	public List<FindingSeverity>? FindingSeverities { get; set; }

	/// <summary>License risk categories to filter by</summary>
	public List<LicenseRiskCategory>? RiskCategories { get; set; }
}
