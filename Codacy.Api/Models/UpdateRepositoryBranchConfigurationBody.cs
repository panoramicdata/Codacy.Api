using System.Text.Json.Serialization;

namespace Codacy.Api.Models;

/// <summary>
/// Update repository branch configuration body
/// </summary>
/// <remarks>
/// Both properties are optional, and unset ones are omitted from the request rather than sent as
/// null: the API treats a property it receives as an instruction, so sending null would clear the
/// setting you did not mean to touch.
/// </remarks>
public class UpdateRepositoryBranchConfigurationBody
{
	/// <summary>Is enabled for analysis</summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public bool? IsEnabled { get; set; }

	/// <summary>
	/// Makes this the repository's default branch. This is the only route that sets a default
	/// branch; there is no separate set-default endpoint.
	/// </summary>
	[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	public bool? IsDefault { get; set; }
}
