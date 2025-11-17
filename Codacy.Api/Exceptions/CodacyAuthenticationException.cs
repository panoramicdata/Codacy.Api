using System.Net;

namespace Codacy.Api.Exceptions;

/// <summary>
/// Exception thrown when authentication fails (401 Unauthorized)
/// </summary>
public class CodacyAuthenticationException : CodacyApiException
{
	/// <summary>
	/// Initializes a new instance of the CodacyAuthenticationException class
	/// </summary>
	public CodacyAuthenticationException() : base("Authentication failed. Please check your API token.")
	{
		StatusCode = HttpStatusCode.Unauthorized;
	}

	/// <summary>
	/// Initializes a new instance of the CodacyAuthenticationException class with a specified error message
	/// </summary>
	/// <param name="message">The message that describes the error</param>
	public CodacyAuthenticationException(string message) : base(message)
	{
		StatusCode = HttpStatusCode.Unauthorized;
	}

	/// <summary>
	/// Initializes a new instance of the CodacyAuthenticationException class with a specified error message and a reference to the inner exception
	/// </summary>
	/// <param name="message">The message that describes the error</param>
	/// <param name="innerException">The exception that is the cause of the current exception</param>
	public CodacyAuthenticationException(string message, Exception innerException) : base(message, innerException)
	{
		StatusCode = HttpStatusCode.Unauthorized;
	}
}
