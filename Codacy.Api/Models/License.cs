namespace Codacy.Api.Models;

/// <summary>
/// Request body to generate a license
/// </summary>
public class License
{
	/// <summary>Number of seats</summary>
	public required int NumberOfSeats { get; set; }

	/// <summary>Email</summary>
	public required string Email { get; set; }

	/// <summary>Expiration date</summary>
	public required DateTimeOffset ExpirationDate { get; set; }

	/// <summary>Inactivity threshold</summary>
	public int? InactivityThreshold { get; set; }

	/// <summary>Whether to automatically add authors</summary>
	public bool? AutoAddAuthors { get; set; }

	/// <summary>Whether to allow seats overflow</summary>
	public bool? AllowSeatsOverflow { get; set; }
}
