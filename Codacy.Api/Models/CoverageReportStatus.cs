using System.Text.Json.Serialization;

namespace Codacy.Api.Models;

/// <summary>
/// Coverage status
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<CoverageReportStatus>))]
public enum CoverageReportStatus
{
	/// <summary>Pending</summary>
	Pending,

	/// <summary>Processed</summary>
	Processed,

	/// <summary>Commit not analysed</summary>
	CommitNotAnalysed,

	/// <summary>Commit not found</summary>
	CommitNotFound,

	/// <summary>Branch not enabled</summary>
	BranchNotEnabled,

	/// <summary>Missing final report</summary>
	MissingFinal
}
