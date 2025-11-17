using System.Net;

namespace Codacy.Api.Exceptions;

/// <summary>
/// Exception thrown when a bad request is made (400 Bad Request)
/// </summary>
public class CodacyBadRequestException : CodacyApiException
{
	/// <summary>
	/// Initializes a new instance of the CodacyBadRequestException class
	/// </summary>
	public CodacyBadRequestException() : base("Bad request. Please check your request parameters.")
	{
		StatusCode = HttpStatusCode.BadRequest;
	}

	/// <summary>
	/// Initializes a new instance of the CodacyBadRequestException class with a specified error message
	/// </summary>
	/// <param name="message">The message that describes the error</param>
	public CodacyBadRequestException(string message) : base(message)
	{
		StatusCode = HttpStatusCode.BadRequest;
	}

	/// <summary>
	/// Initializes a new instance of the CodacyBadRequestException class with a specified error message and a reference to the inner exception
	/// </summary>
	/// <param name="message">The message that describes the error</param>
	/// <param name="innerException">The exception that is the cause of the current exception</param>
	public CodacyBadRequestException(string message, Exception innerException) : base(message, innerException)
	{
		StatusCode = HttpStatusCode.BadRequest;
	}
}
