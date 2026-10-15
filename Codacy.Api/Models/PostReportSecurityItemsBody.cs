using System.Text.Json.Serialization;

namespace Codacy.Api.Models;

/// <summary>
/// Optional filters for exporting security items as CSV. Scan types are plain strings (for example SAST)
/// </summary>
/// <remarks>
/// Codacy rejects explicit nulls on these filters, so every property is omitted when unset.
/// </remarks>
public class PostReportSecurityItemsBody
{
	/// <summary>Repository names to filter by</summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public List<string>? Repositories { get; set; }

	/// <summary>Security issue priorities to filter by</summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public List<SrmPriority>? Priorities { get; set; }

	/// <summary>Security issue statuses to filter by</summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public List<SrmStatus>? Statuses { get; set; }

	/// <summary>Security categories to filter by; use _other_ for issues without a category</summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public List<string>? Categories { get; set; }

	/// <summary>Scan types to filter by (for example SAST, SCA, ContainerSCA, Secrets, IaC, CICD, License, PenTesting, DAST, CSPM)</summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public List<string>? ScanTypes { get; set; }

	/// <summary>Segment IDs to filter by</summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public List<long>? Segments { get; set; }

	/// <summary>Text to search for in security items</summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public string? SearchText { get; set; }
}
