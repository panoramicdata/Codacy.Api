# TestDataManager Usage Guide

## Overview

The `TestDataManager` class is a utility for managing integration test data lifecycle, including:
- Test data verification
- Test data retrieval with retry logic
- Cleanup action registration
- Environment status checking

## Features

### 1. Automatic Retry Logic

The TestDataManager uses **Polly v8** to automatically retry transient API failures:

```csharp
using var testDataManager = GetTestDataManager();

// This call will automatically retry on transient errors (429, 500, 503, etc.)
var repository = await testDataManager.GetTestRepositoryAsync(cancellationToken);
```

**Retry Configuration:**
- Maximum retries: 3 (configurable)
- Exponential backoff: 1s, 2s, 4s
- Jitter: Enabled to prevent thundering herd
- Retried errors: 429, 500, 503, 504, timeouts

### 2. Test Data Verification

Verify that your test environment is properly configured:

```csharp
using var testDataManager = GetTestDataManager();

// Check if repository exists in Codacy
var exists = await testDataManager.VerifyRepositoryExistsAsync();

// Check if repository has been analyzed
var analyzed = await testDataManager.VerifyRepositoryAnalyzedAsync();

// Check if repository has branches
var hasBranches = await testDataManager.VerifyRepositoryHasBranchesAsync();
```

### 3. Environment Status

Get a comprehensive view of your test environment:

```csharp
using var testDataManager = GetTestDataManager();

var status = await testDataManager.GetEnvironmentStatusAsync();

Console.WriteLine(status.ToString());
// Output: Repository: gh/panoramicdata/Codacy.Api.TestRepo | 
//         Exists: True | Analyzed: True | Branches: 3 | Files: 12 | Ready: True

if (!status.IsReady)
{
    // Handle unready environment
    if (!status.RepositoryExists)
    {
        Console.WriteLine("Please add repository to Codacy");
    }
}
```

### 4. Test Data Retrieval

Retrieve test data with built-in retry logic:

```csharp
using var testDataManager = GetTestDataManager();

// Get repository details
var repository = await testDataManager.GetTestRepositoryAsync();

// Get branches
var branches = await testDataManager.GetTestRepositoryBranchesAsync(limit: 10);

// Get default branch
var defaultBranch = await testDataManager.GetDefaultBranchAsync();

// Get files
var files = await testDataManager.GetTestRepositoryFilesAsync(
    branch: "main",
    limit: 20);
```

### 5. Cleanup Actions

Register cleanup actions to be executed when tests complete:

```csharp
using var testDataManager = GetTestDataManager();

// Register cleanup action
testDataManager.RegisterCleanupAction(() =>
{
    // Cleanup code here
    Console.WriteLine("Cleaning up test data");
});

// Cleanup actions execute automatically when disposed
// or call manually:
testDataManager.ExecuteCleanup();
```

### 6. Wait for Analysis

Wait for repository analysis to complete (useful for CI/CD):

```csharp
using var testDataManager = GetTestDataManager();

var analyzed = await testDataManager.WaitForRepositoryAnalysisAsync(
    maxWaitTime: TimeSpan.FromMinutes(5),
    pollingInterval: TimeSpan.FromSeconds(10));

if (analyzed)
{
    // Repository is ready for testing
}
```

## Usage in Tests

### Basic Usage

```csharp
[Fact]
public async Task MyTest_WithTestDataManager_WorksCorrectly()
{
    // Arrange
    using var testDataManager = GetTestDataManager();
    
    // Verify environment is ready
    var status = await testDataManager.GetEnvironmentStatusAsync();
    if (!status.IsReady)
    {
        Output.WriteLine("Test environment not ready - skipping test");
        return;
    }

    // Act
    var repository = await testDataManager.GetTestRepositoryAsync();

    // Assert
    repository.Should().NotBeNull();
}
```

### Advanced Usage with Cleanup

```csharp
[Fact]
public async Task MyTest_WithCleanup_CleansUpProperly()
{
    // Arrange
    using var testDataManager = GetTestDataManager();
    var client = GetClient();
    
    // Create some test data
    var createdData = await CreateTestDataAsync();
    
    // Register cleanup to remove it
    testDataManager.RegisterCleanupAction(() =>
    {
        DeleteTestDataAsync(createdData.Id).Wait();
    });

    // Act
    // ... perform test actions

    // Assert
    // ... verify results
    
    // Cleanup executes automatically on dispose
}
```

### Custom Retry Configuration

```csharp
protected TestDataManager GetCustomTestDataManager(int maxRetries = 5)
{
    var client = GetClient();
    var organization = GetTestOrganization();
    var repository = GetTestRepository();
    var provider = Enum.Parse<Provider>(GetTestProvider());
    
    var loggerProvider = new XunitLoggerProvider(Output, LogLevel.Debug);
    var logger = loggerProvider.CreateLogger("TestDataManager");

    return new TestDataManager(
        client,
        organization,
        repository,
        provider,
        logger,
        maxRetries: maxRetries); // Custom retry count
}
```

## Integration with Existing Tests

To add TestDataManager to existing tests:

### Before:
```csharp
[Fact]
public async Task ListFiles_ReturnsFiles()
{
    using var client = GetClient();
    var provider = Enum.Parse<Provider>(GetTestProvider());
    var orgName = GetTestOrganization();
    var repoName = GetTestRepository();

    try
    {
        var response = await client.Repositories.ListFilesAsync(
            provider, orgName, repoName, cancellationToken: CancellationToken);
        
        response.Should().NotBeNull();
    }
    catch (Refit.ApiException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
    {
        Output.WriteLine($"Repository not found: {ex.Message}");
    }
}
```

### After (with TestDataManager):
```csharp
[Fact]
public async Task ListFiles_ReturnsFiles()
{
    using var testDataManager = GetTestDataManager();
    
    // Verify repository is ready
    if (!await testDataManager.VerifyRepositoryExistsAsync(CancellationToken))
    {
        Output.WriteLine("Repository not found in Codacy - skipping test");
        return;
    }

    // Get files with automatic retry
    var files = await testDataManager.GetTestRepositoryFilesAsync(
        cancellationToken: CancellationToken);
    
    files.Should().NotBeNull();
}
```

## Configuration

The TestDataManager reads configuration from user secrets:

```json
{
  "CodacyApi": {
    "ApiToken": "your-api-token",
    "BaseUrl": "https://app.codacy.com",
    "TestOrganization": "panoramicdata",
    "TestProvider": "gh",
    "TestRepository": "Codacy.Api.TestRepo",
    "TestBranch": "main"
  }
}
```

## Logging

TestDataManager logs all operations when a logger is provided:

```
[INFO] Repository panoramicdata/Codacy.Api.TestRepo verified in Codacy
[WARN] Retry attempt 1 of 3. Waiting 1000ms before next attempt.
[INFO] Repository panoramicdata/Codacy.Api.TestRepo has 3 branches
[INFO] Executing 2 cleanup actions
```

## Best Practices

### 1. Always Use `using` Statement

```csharp
using var testDataManager = GetTestDataManager();
// Ensures cleanup actions execute
```

### 2. Verify Environment Before Tests

```csharp
var status = await testDataManager.GetEnvironmentStatusAsync();
if (!status.IsReady)
{
    Output.WriteLine($"Environment not ready: {status}");
    return; // Skip test gracefully
}
```

### 3. Use Retry Logic for Flaky Operations

```csharp
// Don't manually handle transient errors
var result = await testDataManager.ExecuteWithRetryAsync(async () =>
{
    return await client.SomeApi.FlakyOperationAsync();
});
```

### 4. Register Cleanup Early

```csharp
// Register cleanup immediately after creating resources
var resource = await CreateResourceAsync();
testDataManager.RegisterCleanupAction(() => DeleteResourceAsync(resource.Id).Wait());
```

### 5. Log Environment Status in CI/CD

```csharp
[Fact]
public async Task EnvironmentCheck()
{
    using var testDataManager = GetTestDataManager();
    var status = await testDataManager.GetEnvironmentStatusAsync();
    
    Output.WriteLine(status.ToString());
    
    status.IsReady.Should().BeTrue(
        "Test environment must be ready before running integration tests");
}
```

## Troubleshooting

### Repository Not Found

If `VerifyRepositoryExistsAsync()` returns false:

1. Check that the repository exists in Codacy
2. Verify the organization, repository name, and provider in user secrets
3. Ensure the API token has access to the repository

### Repository Not Analyzed

If `VerifyRepositoryAnalyzedAsync()` returns false:

1. Wait for Codacy to complete initial analysis (can take 5-10 minutes)
2. Use `WaitForRepositoryAnalysisAsync()` in setup tests
3. Check Codacy UI for analysis errors

### Retry Exhausted

If operations fail even with retries:

1. Check Codacy API status: https://status.codacy.com
2. Verify API rate limits haven't been exceeded
3. Check network connectivity
4. Review error logs for non-transient errors

## API Reference

### Constructor

```csharp
public TestDataManager(
    CodacyClient client,
    string testOrganization,
    string testRepository,
    Provider testProvider,
    ILogger? logger = null,
    int maxRetries = DefaultMaxRetries)
```

### Verification Methods

- `Task<bool> VerifyRepositoryExistsAsync(CancellationToken)`
- `Task<bool> VerifyRepositoryAnalyzedAsync(CancellationToken)`
- `Task<bool> VerifyRepositoryHasBranchesAsync(CancellationToken)`

### Retrieval Methods

- `Task<Repository?> GetTestRepositoryAsync(CancellationToken)`
- `Task<List<Branch>> GetTestRepositoryBranchesAsync(int?, CancellationToken)`
- `Task<Branch?> GetDefaultBranchAsync(CancellationToken)`
- `Task<List<FileWithAnalysisInfo>> GetTestRepositoryFilesAsync(string?, int?, CancellationToken)`

### Utility Methods

- `Task<T> ExecuteWithRetryAsync<T>(Func<Task<T>>, CancellationToken)`
- `Task ExecuteWithRetryAsync(Func<Task>, CancellationToken)`
- `void RegisterCleanupAction(Action)`
- `void ExecuteCleanup()`
- `Task<bool> WaitForRepositoryAnalysisAsync(TimeSpan?, TimeSpan?, CancellationToken)`
- `Task<TestEnvironmentStatus> GetEnvironmentStatusAsync(CancellationToken)`

## Examples

See `TestDataManagerExampleTests.cs` for complete working examples.

---

**Version**: 1.0  
**Last Updated**: 2025-01-20  
**Phase**: 1.3 - Test Helper Utilities
