using Microsoft.Extensions.Configuration;

namespace Codacy.Api.Test;

/// <summary>
/// Base class for integration tests that require API credentials
/// </summary>
public abstract class TestBase(ITestOutputHelper output)
{
	protected IConfiguration Configuration { get; } = new ConfigurationBuilder()
			.AddJsonFile("secrets.example.json", optional: true)
			.AddUserSecrets<TestBase>()
			.Build();
	protected ITestOutputHelper Output { get; } = output;

	protected static CancellationToken CancellationToken => TestContext.Current.CancellationToken;

	protected CodacyClientOptions GetClientOptions()
	{
		var apiToken = Configuration["CodacyApi:ApiToken"];

		if (string.IsNullOrWhiteSpace(apiToken))
		{
			throw new InvalidOperationException(
				"API token not configured. Please set up user secrets with 'CodacyApi:ApiToken'. " +
				"See UserSecrets.md for detailed instructions.");
		}

		// Get base URL from configuration or use default
		var baseUrl = Configuration["CodacyApi:BaseUrl"];

		// Create logger for HTTP operations
		var loggerProvider = new XunitLoggerProvider(Output, LogLevel.Debug);
		var logger = loggerProvider.CreateLogger("Codacy.Api.Http");

		// Create HTTP client with logging handler
		var loggingHandler = new LoggingHttpMessageHandler(logger);
		var httpClient = new HttpClient(loggingHandler)
		{
			BaseAddress = new Uri(!string.IsNullOrWhiteSpace(baseUrl) ? baseUrl : "https://app.codacy.com")
		};

		// Add authentication header to the custom HttpClient
		httpClient.DefaultRequestHeaders.Add("api-token", apiToken);

		return new CodacyClientOptions
		{
			ApiToken = apiToken,
			BaseUrl = !string.IsNullOrWhiteSpace(baseUrl) ? baseUrl : "https://app.codacy.com",
			EnableRequestLogging = true,
			EnableResponseLogging = true,
			Logger = logger,
			HttpClient = httpClient
		};
	}

	protected CodacyClient GetClient() => new(GetClientOptions());

	/// <summary>
	/// Gets the test organization name from configuration
	/// </summary>
	protected string GetTestOrganization()
	{
		var org = Configuration["CodacyApi:TestOrganization"];
		if (string.IsNullOrWhiteSpace(org))
		{
			throw new InvalidOperationException(
				"Test organization not configured. Please set 'CodacyApi:TestOrganization' in user secrets.");
		}
		return org;
	}

	/// <summary>
	/// Gets the test provider from configuration (e.g., "gh" for GitHub)
	/// </summary>
	protected string GetTestProvider()
	{
		var provider = Configuration["CodacyApi:TestProvider"];
		if (string.IsNullOrWhiteSpace(provider))
		{
			throw new InvalidOperationException(
				"Test provider not configured. Please set 'CodacyApi:TestProvider' in user secrets.");
		}
		return provider;
	}

	/// <summary>
	/// Gets the test repository name from configuration
	/// </summary>
	protected string GetTestRepository()
	{
		var repo = Configuration["CodacyApi:TestRepository"];
		if (string.IsNullOrWhiteSpace(repo))
		{
			throw new InvalidOperationException(
				"Test repository not configured. Please set 'CodacyApi:TestRepository' in user secrets.");
		}
		return repo;
	}

	/// <summary>
	/// Creates a TestDataManager for managing test data lifecycle
	/// </summary>
	protected TestDataManager GetTestDataManager()
	{
		var client = GetClient();
		var organization = GetTestOrganization();
		var repository = GetTestRepository();
		var provider = Enum.Parse<Provider>(GetTestProvider());

		// Create logger for test data manager
		var loggerProvider = new XunitLoggerProvider(Output, LogLevel.Debug);
		var logger = loggerProvider.CreateLogger("TestDataManager");

		return new TestDataManager(
			client,
			organization,
			repository,
			provider,
			logger);
	}
}
