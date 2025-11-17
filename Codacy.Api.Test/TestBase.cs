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
				"API token not configured. Please set up user secrets with 'CodacyApi:ApiToken'");
		}

		return new CodacyClientOptions
		{
			ApiToken = apiToken,
			EnableRequestLogging = true,
			EnableResponseLogging = true
		};
	}

	protected CodacyClient GetClient() => new(GetClientOptions());
}
