namespace Codacy.Api.Models;

/// <summary>
/// Request body to add a card
/// </summary>
public class CardCreation
{
	/// <summary>Card token</summary>
	public required string CardToken { get; set; }
}
