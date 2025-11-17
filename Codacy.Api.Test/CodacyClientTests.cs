namespace Codacy.Api.Test;

/// <summary>
/// Unit tests for CodacyClient
/// </summary>
public class CodacyClientTests
{
	[Fact]
	public void Constructor_WithValidOptions_CreatesClient()
	{
		// Arrange
		var options = new CodacyClientOptions
		{
			ApiToken = "test-token"
		};

		// Act
		using var client = new CodacyClient(options);

		// Assert
		Assert.NotNull(client);
		Assert.NotNull(client.Organizations);
		Assert.NotNull(client.Repositories);
		Assert.NotNull(client.Analysis);
		Assert.NotNull(client.Issues);
		Assert.NotNull(client.Commits);
		Assert.NotNull(client.PullRequests);
	}

	[Fact]
	public void Constructor_WithNullOptions_ThrowsArgumentNullException()
	{
		// Act & Assert
		Assert.Throws<ArgumentNullException>(() => new CodacyClient(null!));
	}

	[Fact]
	public void Constructor_WithInvalidOptions_ThrowsArgumentException()
	{
		// Arrange
		var options = new CodacyClientOptions
		{
			ApiToken = ""
		};

		// Act & Assert
		Assert.Throws<ArgumentException>(() => new CodacyClient(options));
	}

	[Fact]
	public void Dispose_CanBeCalledMultipleTimes()
	{
		// Arrange
		var options = new CodacyClientOptions
		{
			ApiToken = "test-token"
		};
		var client = new CodacyClient(options);

		// Act & Assert
		client.Dispose();
		client.Dispose(); // Should not throw
	}
}
