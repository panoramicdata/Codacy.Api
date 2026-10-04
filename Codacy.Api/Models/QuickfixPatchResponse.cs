namespace Codacy.Api.Models;

/// <summary>
/// Quick fixes in patch format
/// </summary>
public class QuickfixPatchResponse
{
	/// <summary>Patch data</summary>
	public required QuickfixPatchData Data { get; set; }
}

/// <summary>
/// Quick fix patch data
/// </summary>
public class QuickfixPatchData
{
	/// <summary>Base64 encoded patch file</summary>
	public required string Patch { get; set; }
}
