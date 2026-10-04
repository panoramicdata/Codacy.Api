namespace Codacy.Api.Models;

/// <summary>
/// Optional filters for exporting security items as CSV. Scan types are plain strings (for example SAST)
/// </summary>
public class PostReportSecurityItemsBody
{
	/// <summary>Repository names to filter by</summary>
	public List<string>? Repositories { get; set; }

	/// <summary>Security issue priorities to filter by</summary>
	public List<SrmPriority>? Priorities { get; set; }

	/// <summary>Security issue statuses to filter by</summary>
	public List<SrmStatus>? Statuses { get; set; }

	/// <summary>Security categories to filter by; use _other_ for issues without a category</summary>
	public List<string>? Categories { get; set; }

	/// <summary>Scan types to filter by (for example SAST, SCA, ContainerSCA, Secrets, IaC, CICD, License, PenTesting, DAST, CSPM)</summary>
	public List<string>? ScanTypes { get; set; }

	/// <summary>Segment IDs to filter by</summary>
	public List<long>? Segments { get; set; }

	/// <summary>Text to search for in security items</summary>
	public string? SearchText { get; set; }
}
