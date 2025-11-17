using System.Net;

namespace Codacy.Api.Exceptions;

/// <summary>
/// Exception thrown when a resource is not found (404 Not Found)
/// </summary>
public class CodacyNotFoundException : CodacyApiException
{
	/// <summary>
	/// Initializes a new instance of the CodacyNotFoundException class
	/// </summary>
	public CodacyNotFoundException() : base("The requested resource was not found.")
	{
		StatusCode = HttpStatusCode.NotFound;
	}

	/// <summary>
	/// Initializes a new instance of the CodacyNotFoundException class with a specified error message
	/// </summary>
	/// <param name="message">The message that describes the error</param>
	public CodacyNotFoundException(string message) : base(message)
	{
		StatusCode = HttpStatusCode.NotFound;
	}

	/// <summary>
	/// Initializes a new instance of the CodacyNotFoundException class with a specified error message and a reference to the inner exception
	/// </summary>
	/// <param name="message">The message that describes the error</param>
	/// <param name="innerException">The exception that is the cause of the current exception</param>
	public CodacyNotFoundException(string message, Exception innerException) : base(message, innerException)
	{
		StatusCode = HttpStatusCode.NotFound;
	}
}
