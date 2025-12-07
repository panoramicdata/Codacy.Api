namespace Codacy.Api.Models;

/// <summary>
/// Quality settings response
/// </summary>
public class QualitySettingsResponse
{
	/// <summary>Settings data</summary>
	public required QualitySettingsWithGatePolicy Data { get; set; }
}
