namespace Codacy.Api.Models;

/// <summary>
/// Names of the repositories to link or unlink from a gate policy
/// </summary>
public class ApplyGatePolicyToRepositoriesBody
{
	/// <summary>Names of the repositories to link to a gate policy</summary>
	public required List<string> Link { get; set; }

	/// <summary>Names of the repositories to unlink from a gate policy</summary>
	public required List<string> Unlink { get; set; }
}
