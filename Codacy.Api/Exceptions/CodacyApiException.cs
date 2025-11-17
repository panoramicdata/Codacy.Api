using System.Net;

namespace Codacy.Api.Exceptions;

/// <summary>
/// Base exception for all Codacy API errors
/// </summary>
public class CodacyApiException : Exception
{
	/// <summary>
	/// Initializes a new instance of the CodacyApiException class
	/// </summary>
	public CodacyApiException()
	{
	}

	/// <summary>
	/// Initializes a new instance of the CodacyApiException class with a specified error message
	/// </summary>
	/// <param name="message">The message that describes the error</param>
	public CodacyApiException(string message) : base(message)
	{
	}

	/// <summary>
	/// Initializes a new instance of the CodacyApiException class with a specified error message and a reference to the inner exception
	/// </summary>
	/// <param name="message">The message that describes the error</param>
	/// <param name="innerException">The exception that is the cause of the current exception</param>
	public CodacyApiException(string message, Exception innerException) : base(message, innerException)
	{
	}

	/// <summary>
	/// Gets the HTTP status code associated with the error
	/// </summary>
	public HttpStatusCode? StatusCode { get; init; }

	/// <summary>
	/// Gets additional error details from the API response
	/// </summary>
	public string? ErrorDetails { get; init; }
}
