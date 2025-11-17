# Codacy API .NET Library

[![NuGet Version](https://img.shields.io/nuget/v/Codacy.Api)](https://www.nuget.org/packages/Codacy.Api)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Codacy.Api)](https://www.nuget.org/packages/Codacy.Api)
[![Build Status](https://img.shields.io/github/actions/workflow/status/panoramicdata/Codacy.Api/publish-nuget.yml)](https://github.com/panoramicdata/Codacy.Api/actions)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

A comprehensive, modern .NET library for interacting with the [Codacy](https://www.codacy.com/) API. This library provides full coverage of the Codacy API with a clean, intuitive interface using modern C# patterns and best practices.

## 📚 Official Documentation

- **API Documentation**: [https://docs.codacy.com/codacy-api/](https://docs.codacy.com/codacy-api/)
- **API Reference**: [https://app.codacy.com/api/api-docs](https://app.codacy.com/api/api-docs)
- **Codacy Official Site**: [https://www.codacy.com/](https://www.codacy.com/)

## Features

- 🎯 **Complete API Coverage** - Full support for all Codacy endpoints
- 🚀 **Modern .NET** - Built for .NET 9 with modern C# features
- 🔒 **Type Safety** - Strongly typed models and responses
- 📝 **Comprehensive Logging** - Built-in logging and request/response interception
- 🔄 **Retry Logic** - Automatic retry with exponential backoff
- 📖 **Rich Documentation** - IntelliSense-friendly XML documentation
- ✅ **Thoroughly Tested** - Comprehensive unit and integration tests
- ⚡ **High Performance** - Optimized for efficiency and low memory usage

## Installation

Install the package via NuGet Package Manager:

```bash
dotnet add package Codacy.Api
```

Or via Package Manager Console:

```powershell
Install-Package Codacy.Api
```

## Quick Start

### 1. Authentication Setup

Codacy API uses **API Token authentication**. You'll need:

1. **API Token** - Your personal or project API token

#### Creating API Tokens in Codacy
1. Log into your Codacy account
2. Navigate to **Account Settings** → **Access Management** → **API Tokens**
3. Click **Create API Token**
4. Configure the token:
   - **Name**: Your token identifier
   - **Scope**: Select appropriate permissions for your use case
5. Copy the generated token (it will only be shown once)

```csharp
using Codacy.Api;

var options = new CodacyClientOptions
{
    ApiToken = "your-api-token-here"
};

var client = new CodacyClient(options);
```

### 2. Basic Usage Examples

#### Working with Organizations

```csharp
// Use a CancellationToken for all async operations
using var cts = new CancellationTokenSource();
var cancellationToken = cts.Token;

// Get organization information
var organization = await client.Organizations.GetAsync("your-org-name", cancellationToken);
Console.WriteLine($"Organization: {organization.Name}");

// List all repositories in the organization
var repositories = await client.Organizations.GetRepositoriesAsync("your-org-name", cancellationToken);
foreach (var repo in repositories)
{
    Console.WriteLine($"Repository: {repo.Name}");
}
```

#### Working with Repositories

```csharp
// Get repository information
var repository = await client.Repositories.GetAsync("provider", "organization", "repository", cancellationToken);
Console.WriteLine($"Repository: {repository.Name}");
Console.WriteLine($"Grade: {repository.Grade}");

// Get repository quality overview
var quality = await client.Repositories.GetQualityAsync("provider", "organization", "repository", cancellationToken);
Console.WriteLine($"Quality Issues: {quality.IssuesCount}");
Console.WriteLine($"Coverage: {quality.Coverage}%");
```

#### Working with Analysis

```csharp
// Get latest analysis for a repository
var analysis = await client.Analysis.GetLatestAsync("provider", "organization", "repository", cancellationToken);
Console.WriteLine($"Analysis Date: {analysis.Timestamp}");
Console.WriteLine($"Issues Found: {analysis.NewIssues}");

// Get commit analysis
var commitAnalysis = await client.Commits.GetAsync("provider", "organization", "repository", "commit-sha", cancellationToken);
Console.WriteLine($"Commit Grade: {commitAnalysis.Grade}");
```

#### Working with Issues

```csharp
// Get issues for a repository
var issues = await client.Issues.GetAllAsync("provider", "organization", "repository", cancellationToken);
foreach (var issue in issues)
{
    Console.WriteLine($"Issue: {issue.Message}");
    Console.WriteLine($"Pattern: {issue.PatternId}");
    Console.WriteLine($"File: {issue.File.Path}:{issue.File.Line}");
}
```

### 3. Advanced Configuration

#### Custom HTTP Configuration

```csharp
var options = new CodacyClientOptions
{
    ApiToken = "your-api-token",
    
    // Custom timeout
    RequestTimeout = TimeSpan.FromSeconds(30),
    
    // Custom retry policy
    MaxRetryAttempts = 3,
    RetryDelay = TimeSpan.FromSeconds(1),
    
    // Use custom base URL (for self-hosted instances)
    BaseUrl = "https://your-codacy-instance.com"
};

var client = new CodacyClient(options);
```

#### Logging Configuration

```csharp
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

// Create a service collection with logging
var services = new ServiceCollection();
services.AddLogging(builder => builder.AddConsole().SetMinimumLevel(LogLevel.Debug));

var serviceProvider = services.BuildServiceProvider();
var logger = serviceProvider.GetRequiredService<ILogger<CodacyClient>>();

var options = new CodacyClientOptions
{
    ApiToken = "your-api-token",
    Logger = logger,
    EnableRequestLogging = true,
    EnableResponseLogging = true
};

var client = new CodacyClient(options);
```

### 4. Authentication Troubleshooting

If you're experiencing authentication issues:

1. **Verify API Token**: Ensure your API token is correct and hasn't been revoked
2. **Check Token Permissions**: Verify the token has the necessary scopes for your use case
3. **Token Expiration**: Check if the token has expired (if applicable)
4. **Network Connectivity**: Ensure your application can reach the Codacy API endpoints
5. **Self-Hosted Instances**: Verify the correct base URL if using self-hosted Codacy

### 5. Pagination and Large Result Sets

```csharp
// Handle pagination automatically
var allIssues = new List<Issue>();
var cursor = (string?)null;

do
{
    var response = await client.Issues.GetPageAsync(
        "provider", 
        "organization", 
        "repository",
        cursor: cursor,
        limit: 100,
        cancellationToken);
    
    allIssues.AddRange(response.Data);
    cursor = response.Pagination?.Cursor;
    
} while (!string.IsNullOrEmpty(cursor));

Console.WriteLine($"Retrieved {allIssues.Count} total issues");
```

### 6. Error Handling

```csharp
try
{
    var repository = await client.Repositories.GetAsync("provider", "org", "repo", cancellationToken);
}
catch (CodacyNotFoundException ex)
{
    Console.WriteLine($"Repository not found: {ex.Message}");
}
catch (CodacyAuthenticationException ex)
{
    Console.WriteLine($"Authentication failed: {ex.Message}");
    Console.WriteLine("Check your API token and permissions");
}
catch (CodacyApiException ex)
{
    Console.WriteLine($"API error: {ex.Message}");
    Console.WriteLine($"Status code: {ex.StatusCode}");
    Console.WriteLine($"Error details: {ex.ErrorDetails}");
}
```

## API Coverage

This library provides comprehensive coverage of the Codacy API, organized into logical groups. For complete API endpoint documentation, refer to the [official API documentation](https://docs.codacy.com/codacy-api/).

### Core Modules

- **Organizations** - Organization management and information
- **Repositories** - Repository analysis and configuration
- **Analysis** - Code quality analysis results and trends
- **Issues** - Code quality issues, patterns, and suppression
- **Commits** - Commit analysis and quality evolution
- **Pull Requests** - PR quality gates, analysis, and suggestions
- **Coverage** - Code coverage reports and trends
- **Patterns** - Code pattern management and configuration

## Configuration Options

The `CodacyClientOptions` class provides extensive configuration:

```csharp
public class CodacyClientOptions
{
    // Required authentication
    public required string ApiToken { get; init; }
    
    // Optional configuration
    public string BaseUrl { get; init; } = "https://app.codacy.com";
    public TimeSpan RequestTimeout { get; init; } = TimeSpan.FromSeconds(30);
    public int MaxRetryAttempts { get; init; } = 3;
    public TimeSpan RetryDelay { get; init; } = TimeSpan.FromSeconds(1);
    public ILogger? Logger { get; init; } = null;
    
    // Advanced options
    public bool EnableRequestLogging { get; init; } = false;
    public bool EnableResponseLogging { get; init; } = false;
    public IReadOnlyDictionary<string, string> DefaultHeaders { get; init; } = new Dictionary<string, string>();
    public bool UseExponentialBackoff { get; init; } = true;
    public TimeSpan MaxRetryDelay { get; init; } = TimeSpan.FromSeconds(30);
}
```

## API Reference

For detailed API endpoint documentation, parameters, and response formats, please refer to the official resources:

- 📖 **[Codacy API Documentation](https://docs.codacy.com/codacy-api/)** - Complete API reference
- 🔐 **[Authentication Guide](https://docs.codacy.com/codacy-api/api-tokens/)** - How to obtain and use API tokens
- 🌐 **[Codacy Platform](https://www.codacy.com/)** - Official product documentation

## Contributing

We welcome contributions from the community! Here's how you can help:

### Development Setup

1. **Clone the repository**:
   ```bash
   git clone https://github.com/panoramicdata/Codacy.Api.git
   cd Codacy.Api
   ```

2. **Install .NET 9 SDK**:
   Download from [dotnet.microsoft.com](https://dotnet.microsoft.com/download)

3. **Set up User Secrets for testing**:
   ```bash
   cd Codacy.Api.Test
   dotnet user-secrets init
   dotnet user-secrets set "CodacyApi:ApiToken" "your-test-api-token"
   ```

4. **Build and test**:
   ```bash
   dotnet build
   dotnet test
   ```

### Publishing to NuGet

For maintainers who need to publish new versions:

📦 **See [PUBLISHING.md](PUBLISHING.md)** for complete publishing instructions

Quick publish:
```powershell
.\Publish.ps1
```

### Coding Standards

- **Follow the project's coding standards** as defined in `copilot-instructions.md`
- **Use modern C# patterns** (primary constructors, collection expressions, etc.)
- **Maintain zero warnings policy** - all code must compile without warnings
- **Write comprehensive tests** - both unit and integration tests
- **Document public APIs** - use XML documentation comments

### Pull Request Process

1. **Fork the repository** and create a feature branch
2. **Write tests** for all new functionality
3. **Ensure all tests pass** including integration tests
4. **Update documentation** as needed
5. **Submit a pull request** with a clear description of changes

## Support

- **Official Documentation**: [Codacy API Docs](https://docs.codacy.com/codacy-api/)
- **GitHub Issues**: [Report Issues](https://github.com/panoramicdata/Codacy.Api/issues)
- **GitHub Discussions**: [Community Support](https://github.com/panoramicdata/Codacy.Api/discussions)
- **Codacy Support**: Contact Codacy for API access and account issues

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Copyright

Copyright © 2025 Panoramic Data Limited. All rights reserved.

## Changelog

See [CHANGELOG.md](CHANGELOG.md) for a detailed history of changes and releases.
