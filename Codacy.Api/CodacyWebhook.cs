using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Codacy.Api.Models;

namespace Codacy.Api;

/// <summary>
/// Helpers for receiving Codacy webhook deliveries: verifying the signature and reading the
/// payload. Codacy signs the raw request body with the secret returned when the endpoint was
/// created, so verify the bytes exactly as received, before any re-serialization.
/// </summary>
public static class CodacyWebhook
{
	/// <summary>Name of the header carrying the signature, <c>sha256=&lt;hex&gt;</c></summary>
	public const string SignatureHeader = "X-Codacy-Signature";

	/// <summary>Name of the header carrying a unique identifier per delivery attempt</summary>
	public const string DeliveryHeader = "X-Codacy-Delivery";

	/// <summary>The only event Codacy currently sends</summary>
	public const string AnalysisCompletedEvent = "quality.analysis.completed";

	private const string SignaturePrefix = "sha256=";

	/// <summary>
	/// Verifies a delivery's signature in constant time
	/// </summary>
	/// <param name="body">The raw request body, exactly as received</param>
	/// <param name="signatureHeader">The value of <see cref="SignatureHeader"/>; null or malformed values fail verification</param>
	/// <param name="secret">The endpoint secret from <see cref="WebhookEndpointCreated.Secret"/></param>
	/// <returns>True if the signature matches</returns>
	public static bool VerifySignature(ReadOnlySpan<byte> body, string? signatureHeader, string secret)
	{
		ArgumentException.ThrowIfNullOrEmpty(secret);

		if (signatureHeader is null
			|| !signatureHeader.StartsWith(SignaturePrefix, StringComparison.OrdinalIgnoreCase))
		{
			return false;
		}

		byte[] provided;
		try
		{
			provided = Convert.FromHexString(signatureHeader.AsSpan(SignaturePrefix.Length));
		}
		catch (FormatException)
		{
			return false;
		}

		var expected = HMACSHA256.HashData(Encoding.UTF8.GetBytes(secret), body);
		return CryptographicOperations.FixedTimeEquals(expected, provided);
	}

	/// <summary>
	/// Verifies a delivery's signature in constant time
	/// </summary>
	/// <param name="body">The raw request body, exactly as received</param>
	/// <param name="signatureHeader">The value of <see cref="SignatureHeader"/></param>
	/// <param name="secret">The endpoint secret from <see cref="WebhookEndpointCreated.Secret"/></param>
	/// <returns>True if the signature matches</returns>
	public static bool VerifySignature(string body, string? signatureHeader, string secret)
		=> VerifySignature(Encoding.UTF8.GetBytes(body), signatureHeader, secret);

	/// <summary>
	/// Reads a <c>quality.analysis.completed</c> delivery body
	/// </summary>
	/// <param name="body">The request body</param>
	/// <returns>The payload</returns>
	/// <exception cref="JsonException">The body is not a valid payload</exception>
	public static WebhookAnalysisCompleted Deserialize(string body)
		=> JsonSerializer.Deserialize<WebhookAnalysisCompleted>(body, CodacyClient.JsonSerializerOptions)
			?? throw new JsonException("The webhook body was null.");
}
