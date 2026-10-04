namespace Codacy.Api.Models;

/// <summary>
/// Codacy checks required before merge on a branch
/// </summary>
public class BranchRequiredChecks
{
	/// <summary>True if the branch requires the Codacy Static Code Analysis step before merge</summary>
	public required bool Quality { get; set; }

	/// <summary>True if the branch requires the Codacy Diff Coverage step before merge</summary>
	public required bool DiffCoverage { get; set; }

	/// <summary>True if the branch requires the Codacy Coverage Variation step before merge</summary>
	public required bool CoverageVariation { get; set; }
}
