namespace Codacy.Api.Test;

/// <summary>
/// Unit tests for CodacyClientOptions
/// </summary>
public class CodacyClientOptionsTests
{
	[Fact]
	public void Validate_WithValidOptions_DoesNotThrow()
	{
		// Arrange
		var options = new CodacyClientOptions
		{
			ApiToken = "test-token-123"
		};

		// Act & Assert
		var exception = Record.Exception(() => options.Validate());
		Assert.Null(exception);
	}

	[Fact]
	public void Validate_WithEmptyApiToken_ThrowsArgumentException()
	{
		// Arrange
		var options = new CodacyClientOptions
		{
			ApiToken = ""
		};

		// Act & Assert
		var exception = Assert.Throws<ArgumentException>(() => options.Validate());
		Assert.Contains("ApiToken", exception.Message);
	}

	[Fact]
	public void Validate_WithNegativeTimeout_ThrowsArgumentException()
	{
		// Arrange
		var options = new CodacyClientOptions
		{
			ApiToken = "test-token",
			RequestTimeout = TimeSpan.FromSeconds(-1)
		};

		// Act & Assert
		var exception = Assert.Throws<ArgumentException>(() => options.Validate());
		Assert.Contains("RequestTimeout", exception.Message);
	}

	[Fact]
	public void Validate_WithNegativeRetryAttempts_ThrowsArgumentException()
	{
		// Arrange
		var options = new CodacyClientOptions
		{
			ApiToken = "test-token",
			MaxRetryAttempts = -1
		};

		// Act & Assert
		var exception = Assert.Throws<ArgumentException>(() => options.Validate());
		Assert.Contains("MaxRetryAttempts", exception.Message);
	}

	[Fact]
	public void DefaultBaseUrl_ShouldBeAppCodacyCom()
	{
		// Arrange & Act
		var options = new CodacyClientOptions
		{
			ApiToken = "test-token"
		};

		// Assert
		Assert.Equal("https://app.codacy.com", options.BaseUrl);
	}

	[Fact]
	public void DefaultRequestTimeout_ShouldBe30Seconds()
	{
		// Arrange & Act
		var options = new CodacyClientOptions
		{
			ApiToken = "test-token"
		};

		// Assert
		Assert.Equal(TimeSpan.FromSeconds(30), options.RequestTimeout);
	}

	[Fact]
	public void DefaultMaxRetryAttempts_ShouldBe3()
	{
		// Arrange & Act
		var options = new CodacyClientOptions
		{
			ApiToken = "test-token"
		};

		// Assert
		Assert.Equal(3, options.MaxRetryAttempts);
	}
}
