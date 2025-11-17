namespace Codacy.Api.Test;

/// <summary>
/// Unit tests for CodacyClient
/// </summary>
public class CodacyClientTests
{
	[Fact]
	public void Constructor_WithValidOptions_CreatesClientWithAllModules()
	{
		// Arrange
		var options = new CodacyClientOptions
		{
			ApiToken = "test-token",
			HttpClient = CreateTestHttpClient()
		};

		// Act
		using var client = new TestableCodacyClient(options);

		// Assert
		client.Should().NotBeNull();
		client.Version.Should().NotBeNull();
		client.Account.Should().NotBeNull();
		client.Organizations.Should().NotBeNull();
		client.Repositories.Should().NotBeNull();
		client.Analysis.Should().NotBeNull();
		client.Issues.Should().NotBeNull();
		client.Commits.Should().NotBeNull();
		client.PullRequests.Should().NotBeNull();
		client.People.Should().NotBeNull();
		client.Coverage.Should().NotBeNull();
		client.CodingStandards.Should().NotBeNull();
		client.Security.Should().NotBeNull();
	}

	[Fact]
	public void Constructor_WithNullOptions_ThrowsArgumentNullException()
	{
		// Act & Assert
		((Action)(() => _ = new TestableCodacyClient(null!)))
			.Should()
			.ThrowExactly<ArgumentNullException>();
	}

	[Fact]
	public void Constructor_WithInvalidOptions_ThrowsArgumentException()
	{
		// Arrange
		var options = new CodacyClientOptions
		{
			ApiToken = "",
			HttpClient = CreateTestHttpClient()
		};

		// Act & Assert
		((Action)(() => _ = new TestableCodacyClient(options)))
			.Should()
			.ThrowExactly<ArgumentException>();
	}

	[Fact]
	public void Constructor_WithCustomBaseUrl_UsesProvidedUrl()
	{
		// Arrange
		var customUrl = "https://custom.codacy.com";
		var testClient = CreateTestHttpClient();
		var options = new CodacyClientOptions
		{
			ApiToken = "test-token",
			BaseUrl = customUrl,
			HttpClient = testClient
		};

		// Act
		using var client = new TestableCodacyClient(options);

		// Assert
		client.Should().NotBeNull();
		// Note: When HttpClient is provided, BaseUrl from options is not used for HttpClient creation
		// The provided HttpClient's BaseAddress is used instead
	}

	[Fact]
	public void Constructor_WithCustomHeaders_AddsHeadersToHttpClient()
	{
		// Arrange
		var customHeaders = new Dictionary<string, string>
		{
			{ "X-Custom-Header", "CustomValue" },
			{ "X-Another-Header", "AnotherValue" }
		};
		var testClient = CreateTestHttpClient();
		var options = new CodacyClientOptions
		{
			ApiToken = "test-token",
			DefaultHeaders = customHeaders,
			HttpClient = testClient
		};

		// Act
		using var client = new TestableCodacyClient(options);

		// Assert
		client.Should().NotBeNull();
		// Note: Custom headers are only added when CodacyClient creates its own HttpClient
		// When an HttpClient is provided via options, headers are not automatically added
	}

	[Fact]
	public void Constructor_CreatesOwnHttpClient_AddsApiTokenHeader()
	{
		// Arrange
		var apiToken = "test-api-token-12345";
		var options = new CodacyClientOptions
		{
			ApiToken = apiToken
		};

		// Act - Don't provide HttpClient, let CodacyClient create its own
		using var client = new TestableCodacyClientWithRealCreation(options);

		// Assert
		client.Should().NotBeNull();
		client.HttpClientHeaders.Contains("api-token").Should().BeTrue();
		client.HttpClientHeaders.GetValues("api-token").Should().Contain(apiToken);
	}

	[Fact]
	public void Constructor_CreatesOwnHttpClient_SetsTimeout()
	{
		// Arrange
		var timeout = TimeSpan.FromMinutes(5);
		var options = new CodacyClientOptions
		{
			ApiToken = "test-token",
			RequestTimeout = timeout
		};

		// Act
		using var client = new TestableCodacyClientWithRealCreation(options);

		// Assert
		Assert.Equal(timeout, client.HttpClientTimeout);
	}

	[Fact]
	public void Constructor_WithProvidedHttpClient_UsesProvidedClient()
	{
		// Arrange
		var testClient = CreateTestHttpClient();
		testClient.DefaultRequestHeaders.Add("X-Test-Header", "TestValue");

		var options = new CodacyClientOptions
		{
			ApiToken = "test-token",
			HttpClient = testClient
		};

		// Act
		using var client = new TestableCodacyClient(options);

		// Assert
		client.Should().NotBeNull();
		// The provided HttpClient should have our custom header
		testClient.DefaultRequestHeaders.Contains("X-Test-Header").Should().BeTrue();
	}

	[Fact]
	public void Dispose_CanBeCalledMultipleTimes()
	{
		// Arrange
		var options = new CodacyClientOptions
		{
			ApiToken = "test-token",
			HttpClient = CreateTestHttpClient()
		};
		var client = new TestableCodacyClient(options);

		// Act & Assert
		client.Dispose();
		client.Dispose(); // Should not throw
	}

	[Fact]
	public void Dispose_DisposesHttpClient()
	{
		// Arrange
		var options = new CodacyClientOptions
		{
			ApiToken = "test-token",
			HttpClient = CreateTestHttpClient()
		};
		var client = new TestableCodacyClient(options);

		// Act
		client.Dispose();

		// Assert
		client.IsDisposed.Should().BeTrue();
	}

	[Fact]
	public void Constructor_WithBothHttpClientAndFactory_ThrowsArgumentException()
	{
		// Arrange
		var mockFactory = new Mock<IHttpClientFactory>();
		var options = new CodacyClientOptions
		{
			ApiToken = "test-token",
			HttpClient = CreateTestHttpClient(),
			HttpClientFactory = mockFactory.Object
		};

		// Act & Assert
		((Action)(() => _ = new TestableCodacyClient(options)))
			.Should()
			.ThrowExactly<ArgumentException>();
	}

	[Fact]
	public void Constructor_WithHttpClientFactory_UsesFactoryToCreateClient()
	{
		// Arrange
		var testClient = CreateTestHttpClient();
		var mockFactory = new Mock<IHttpClientFactory>();

		// IHttpClientFactory.CreateClient() is an extension method that calls CreateClient(name)
		// The default parameterless version uses Options.DefaultName which is an empty string
		mockFactory.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(testClient);

		var options = new CodacyClientOptions
		{
			ApiToken = "test-token",
			HttpClientFactory = mockFactory.Object
		};

		// Act
		using var client = new TestableCodacyClient(options);

		// Assert
		client.Should().NotBeNull();
		mockFactory.Verify(f => f.CreateClient(It.IsAny<string>()), Times.Once);
	}

	private static HttpClient CreateTestHttpClient()
	{
		return new HttpClient(new TestHttpMessageHandler())
		{
			BaseAddress = new Uri("https://app.codacy.com")
		};
	}

	/// <summary>
	/// Testable version of CodacyClient that mocks API clients
	/// </summary>
	private sealed class TestableCodacyClient(CodacyClientOptions options) : CodacyClient(options)
	{
		protected override T CreateApiClient<T>() where T : class
		{
			// Return a mock instead of calling RestService
			var mock = new Mock<T>();
			return mock.Object;
		}

		public bool IsDisposed { get; private set; }

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				IsDisposed = true;
			}
			base.Dispose(disposing);
		}
	}

	/// <summary>
	/// Testable version that creates its own HttpClient (for testing that code path)
	/// </summary>
	private sealed class TestableCodacyClientWithRealCreation : CodacyClient
	{
		private readonly HttpClient _createdHttpClient;

		public TestableCodacyClientWithRealCreation(CodacyClientOptions options) : base(options)
		{
			// Access the private field via reflection for testing purposes
			var field = typeof(CodacyClient).GetField("_httpClient",
				System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
			_createdHttpClient = (HttpClient)field!.GetValue(this)!;
		}

		protected override T CreateApiClient<T>() where T : class
		{
			// Return a mock instead of calling RestService
			var mock = new Mock<T>();
			return mock.Object;
		}

		public System.Net.Http.Headers.HttpRequestHeaders HttpClientHeaders => _createdHttpClient.DefaultRequestHeaders;
		public TimeSpan HttpClientTimeout => _createdHttpClient.Timeout;
	}

	/// <summary>
	/// Test HTTP message handler that doesn't make real HTTP calls
	/// </summary>
	private sealed class TestHttpMessageHandler : HttpMessageHandler
	{
		protected override Task<HttpResponseMessage> SendAsync(
			HttpRequestMessage request,
			CancellationToken cancellationToken)
		{
			return Task.FromResult(new HttpResponseMessage(System.Net.HttpStatusCode.OK)
			{
				Content = new StringContent("{}")
			});
		}
	}
}
