using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Codacy.Api.Test;

/// <summary>
/// Credential-free tests: a refused write throws before any request is sent.
/// </summary>
public class ReadOnlyOptionTests
{
	private const string Org = "/api/v3/organizations/gh/acme";

	private static CodacyClientOptions Options(Uri uri, bool isReadOnly) => new()
	{
		ApiToken = "fake-token-for-tests",
		BaseUrl = uri.ToString(),
		MaxRetryAttempts = 0,
		IsReadOnly = isReadOnly
	};

	[Fact]
	public void IsReadOnly_DefaultsToFalse()
		=> new CodacyClientOptions { ApiToken = "x" }.IsReadOnly.Should().BeFalse();

	[Fact]
	public void IsReadOnly_WithSuppliedHttpClient_FailsValidation()
	{
		var options = new CodacyClientOptions { ApiToken = "x", HttpClient = new HttpClient(), IsReadOnly = true };

		((Action)(() => options.Validate())).Should().ThrowExactly<ArgumentException>().WithMessage("*IsReadOnly*");
	}

	[Fact]
	public async Task WriteIsRefused_WhenIsReadOnly_AndNothingIsSent()
	{
		using var server = new CountingServer();
		using var client = new CodacyClient(Options(server.BaseUri, isReadOnly: true));

		var act = () => client.Repositories.DeleteRepositoryAsync(Provider.Github, "acme", "repo", TestContext.Current.CancellationToken);

		await act.Should().ThrowAsync<InvalidOperationException>();
		server.RequestCount.Should().Be(0);
	}

	[Fact]
	public async Task WriteIsSent_WhenNotReadOnly()
	{
		using var server = new CountingServer();
		using var client = new CodacyClient(Options(server.BaseUri, isReadOnly: false));

		await client.Repositories.DeleteRepositoryAsync(Provider.Github, "acme", "repo", TestContext.Current.CancellationToken);

		server.RequestCount.Should().Be(1);
	}

	[Fact]
	public async Task ReadStillGoesThrough_WhenIsReadOnly()
	{
		using var server = new CountingServer();
		using var client = new CodacyClient(Options(server.BaseUri, isReadOnly: true));

		var version = await client.Version.GetVersionAsync(TestContext.Current.CancellationToken);

		version.Should().NotBeNull();
		server.RequestCount.Should().Be(1);
	}

	[Theory]
	[InlineData("POST", Org + "/repositories/repo/follow", false)]
	[InlineData("POST", Org + "/people/remove", false)]
	[InlineData("POST", Org + "/billing", false)]
	[InlineData("POST", Org + "/metrics/start", false)]
	[InlineData("PUT", Org + "/repositories/repo/settings/quality/repository", false)]
	[InlineData("DELETE", Org + "/repositories/repo", false)]
	[InlineData("PATCH", Org + "/repositories/repo", false)]
	[InlineData("POST", "/api/v3/analysis/organizations/gh/acme/repositories/repo/issues/search", true)]
	[InlineData("POST", "/api/v3/analysis/organizations/gh/acme/repositories/repo/issues/overview", true)]
	[InlineData("POST", Org + "/security/items/search?cursor=1", true)]
	[InlineData("POST", Org + "/security/dashboard", true)]
	[InlineData("POST", Org + "/metrics/open-issues/latest", true)]
	[InlineData("POST", "/api/v3/search/analysis/organizations/gh/acme/repositories", true)]
	[InlineData("GET", Org + "/repositories", true)]
	[InlineData("POST", "/api/v3/session/heartbeat", true)]
	public async Task ReadOnlyHandler_AllowsOnlyReads(string method, string path, bool expectedSent)
	{
		using var server = new CountingServer();
		using var http = new HttpClient(new LoggingHttpClientHandler(Options(server.BaseUri, isReadOnly: true)))
		{
			BaseAddress = server.BaseUri
		};
		using var request = new HttpRequestMessage(new HttpMethod(method), path);

		if (expectedSent)
		{
			using var response = await http.SendAsync(request, TestContext.Current.CancellationToken);
			response.IsSuccessStatusCode.Should().BeTrue();
			server.RequestCount.Should().Be(1);
		}
		else
		{
			var act = () => http.SendAsync(request, TestContext.Current.CancellationToken);
			await act.Should().ThrowAsync<InvalidOperationException>();
			server.RequestCount.Should().Be(0);
		}
	}

	private sealed class CountingServer : IDisposable
	{
		private readonly HttpListener _listener = new();
		private int _requestCount;

		public Uri BaseUri { get; }

		public int RequestCount => Volatile.Read(ref _requestCount);

		public CountingServer()
		{
			var probe = new TcpListener(IPAddress.Loopback, 0);
			probe.Start();
			var port = ((IPEndPoint)probe.LocalEndpoint).Port;
			probe.Stop();

			BaseUri = new Uri($"http://127.0.0.1:{port}/");
			_listener.Prefixes.Add(BaseUri.AbsoluteUri);
			_listener.Start();
			_ = Task.Run(ServeAsync);
		}

		private async Task ServeAsync()
		{
			while (_listener.IsListening)
			{
				HttpListenerContext context;
				try
				{
					context = await _listener.GetContextAsync().ConfigureAwait(false);
				}
				catch (Exception e) when (e is HttpListenerException or ObjectDisposedException or InvalidOperationException)
				{
					return;
				}

				Interlocked.Increment(ref _requestCount);
				var body = Encoding.UTF8.GetBytes("{\"data\":\"1.0\"}");
				context.Response.ContentType = "application/json";
				await context.Response.OutputStream.WriteAsync(body).ConfigureAwait(false);
				context.Response.Close();
			}
		}

		public void Dispose() => _listener.Close();
	}
}
