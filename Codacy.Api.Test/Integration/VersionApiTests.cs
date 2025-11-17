namespace Codacy.Api.Test.Integration;

/// <summary>
/// Integration tests for Version API
/// </summary>
[Trait("Category", "Integration")]
public class VersionApiTests(ITestOutputHelper output) : TestBase(output)
{
	[Fact]
	public async Task GetVersion_ReturnsVersionInformation()
	{
		// Arrange
		using var client = GetClient();

		// Act
		var response = await client.Version.GetVersionAsync(CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNullOrWhiteSpace();
	}

	[Fact]
	public async Task GetVersion_VersionHasExpectedFormat()
	{
		// Arrange
		using var client = GetClient();

		// Act
		var response = await client.Version.GetVersionAsync(CancellationToken);

		// Assert
		response.Should().NotBeNull();
		response.Data.Should().NotBeNull();
		// Version can be "latest" or in format like "1.0.0"
		(response.Data == "latest" || System.Text.RegularExpressions.Regex.IsMatch(response.Data, @"\d+\.\d+")).Should().BeTrue(
			$"Version should be 'latest' or match format 'X.Y', but was: {response.Data}");
	}
}
