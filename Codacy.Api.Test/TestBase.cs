using Microsoft.Extensions.Configuration;

namespace Codacy.Api.Test;

/// <summary>
/// Base class for integration tests that require API credentials
/// </summary>
public abstract class TestBase
{
	protected IConfiguration Configuration { get; }

	protected TestBase()
	{
		Configuration = new ConfigurationBuilder()
			.AddJsonFile("secrets.example.json", optional: true)
			.AddUserSecrets<TestBase>()
			.Build();
	}

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

		return new CodacyClientOptions
		{
			ApiToken = apiToken,
			BaseUrl = !string.IsNullOrWhiteSpace(baseUrl) ? baseUrl : "https://app.codacy.com",
			EnableRequestLogging = true,
			EnableResponseLogging = true
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
}
