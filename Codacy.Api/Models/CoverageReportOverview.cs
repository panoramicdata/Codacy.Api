namespace Codacy.Api.Models;

/// <summary>
/// Latest coverage reports of a repository
/// </summary>
public class CoverageReportOverview
{
	/// <summary>True if the Quality evolution chart of the repository includes coverage information</summary>
	public bool? HasCoverageOverview { get; set; }

	/// <summary>Last coverage reports</summary>
	public List<RepositoryCoverageReport>? LastReports { get; set; }
}
