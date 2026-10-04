namespace Codacy.Api.Models;

/// <summary>
/// A before/after count pair for a metric impacted by an autoconfig run
/// </summary>
public class AutoconfigBeforeAfter
{
	/// <summary>Value before the run</summary>
	public required int Before { get; set; }

	/// <summary>Value after the run</summary>
	public required int After { get; set; }
}
