using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Codacy.Api.Test.Models;

/// <summary>
/// Tests for the webhook models and the delivery verifier, using bodies taken from the Codacy
/// webhook documentation and API reference.
/// </summary>
public class WebhookTests
{
	private static JsonSerializerOptions Options => CodacyClient.JsonSerializerOptions;

	private const string PullRequestBody = """
		{
		  "event": "quality.analysis.completed",
		  "repository": { "name": "engine" },
		  "organization": { "id": 123456 },
		  "target": { "type": "pullRequest", "value": "464" },
		  "commitSha": "a1b2c3d4e5f60718293a4b5c6d7e8f9012345678",
		  "status": "partial_success",
		  "timestamp": "2025-09-17T23:00:00Z"
		}
		""";

	private static string Sign(string body, string secret) =>
		"sha256=" + Convert.ToHexStringLower(HMACSHA256.HashData(Encoding.UTF8.GetBytes(secret), Encoding.UTF8.GetBytes(body)));

	[Fact]
	public void Deserialize_PullRequestDelivery_ReadsEveryField()
	{
		var payload = CodacyWebhook.Deserialize(PullRequestBody);

		payload.Event.Should().Be(CodacyWebhook.AnalysisCompletedEvent);
		payload.Repository.Name.Should().Be("engine");
		payload.Organization.Id.Should().Be(123456);
		payload.Target.Type.Should().Be(WebhookTargetType.PullRequest);
		payload.Target.Value.Should().Be("464");
		payload.CommitSha.Should().Be("a1b2c3d4e5f60718293a4b5c6d7e8f9012345678");
		payload.Status.Should().Be(WebhookAnalysisStatus.PartialSuccess);
		payload.Timestamp.Should().Be(DateTimeOffset.Parse("2025-09-17T23:00:00Z", CultureInfo.InvariantCulture));
	}

	[Theory]
	[InlineData("success", WebhookAnalysisStatus.Success)]
	[InlineData("failure", WebhookAnalysisStatus.Failure)]
	public void Deserialize_BranchDelivery_ReadsStatusAndTarget(string status, WebhookAnalysisStatus expected)
	{
		var body = PullRequestBody
			.Replace("partial_success", status, StringComparison.Ordinal)
			.Replace("pullRequest", "branch", StringComparison.Ordinal);

		var payload = CodacyWebhook.Deserialize(body);

		payload.Status.Should().Be(expected);
		payload.Target.Type.Should().Be(WebhookTargetType.Branch);
	}

	[Fact]
	public void WebhookEndpointCreated_ReadsSecret()
	{
		const string json = """
			{
			  "id": "80f64371-e6bc-4d9b-b022-7c873cc5e39f",
			  "url": "https://example.com/webhooks/codacy",
			  "createdAt": "2020-11-09T09:10:00Z",
			  "secret": "3n8fVhZ2k9m1QpXeYtR7wLdCsUbGjNoA"
			}
			""";

		var created = JsonSerializer.Deserialize<WebhookEndpointCreated>(json, Options)!;

		created.Id.Should().Be(Guid.Parse("80f64371-e6bc-4d9b-b022-7c873cc5e39f"));
		created.Url.Should().Be("https://example.com/webhooks/codacy");
		created.Secret.Should().Be("3n8fVhZ2k9m1QpXeYtR7wLdCsUbGjNoA");
	}

	[Fact]
	public void WebhookEndpointList_ReadsCountAndLimit()
	{
		const string json = """
			{
			  "data": [ { "id": "80f64371-e6bc-4d9b-b022-7c873cc5e39f", "url": "https://example.com/webhooks/codacy", "createdAt": "2020-11-09T09:10:00Z" } ],
			  "count": 2,
			  "limit": 10
			}
			""";

		var list = JsonSerializer.Deserialize<WebhookEndpointList>(json, Options)!;

		list.Data.Should().ContainSingle();
		list.Count.Should().Be(2);
		list.Limit.Should().Be(10);
	}

	[Fact]
	public void VerifySignature_ValidSignature_ReturnsTrue() =>
		CodacyWebhook.VerifySignature(PullRequestBody, Sign(PullRequestBody, "s3cret"), "s3cret").Should().BeTrue();

	[Fact]
	public void VerifySignature_TamperedBody_ReturnsFalse() =>
		CodacyWebhook.VerifySignature(PullRequestBody + " ", Sign(PullRequestBody, "s3cret"), "s3cret").Should().BeFalse();

	[Fact]
	public void VerifySignature_WrongSecret_ReturnsFalse() =>
		CodacyWebhook.VerifySignature(PullRequestBody, Sign(PullRequestBody, "other"), "s3cret").Should().BeFalse();

	[Theory]
	[InlineData(null)]
	[InlineData("")]
	[InlineData("deadbeef")]
	[InlineData("sha256=not-hex")]
	[InlineData("sha256=")]
	public void VerifySignature_MissingOrMalformedHeader_ReturnsFalse(string? header) =>
		CodacyWebhook.VerifySignature(PullRequestBody, header, "s3cret").Should().BeFalse();
}
