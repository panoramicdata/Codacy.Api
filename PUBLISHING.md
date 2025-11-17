# Publishing to NuGet

This guide explains how to publish new versions of the Codacy.Api package to NuGet.org.

## Prerequisites

1. **NuGet API Key**: You need a valid API key from NuGet.org
   - Go to https://www.nuget.org/account/apikeys
   - Create a new API key with "Push" permissions for the Codacy.Api package
   - Save the key securely

2. **Git Repository**: Ensure your working directory is clean (all changes committed)

3. **Tests Passing**: All unit tests should pass before publishing

## Setup

1. **Create the API key file**:
   ```powershell
   # Copy the template
   Copy-Item nuget-key.txt.example nuget-key.txt
   
   # Edit nuget-key.txt and replace with your actual API key
   notepad nuget-key.txt
   ```

2. **Verify the key file is gitignored**:
   ```powershell
   git status
   # nuget-key.txt should NOT appear in the list
   ```

## Publishing a New Version

### Standard Publish (Recommended)

This will:
- Check that git working directory is clean
- Restore dependencies
- Build the solution in Release mode
- Run all unit tests
- Create the NuGet package
- Prompt for confirmation
- Publish to NuGet.org

```powershell
.\Publish.ps1
```

### Skip Tests

If you need to publish without running tests (not recommended):

```powershell
.\Publish.ps1 -SkipTests
```

### Force Publish

To publish even with uncommitted changes (not recommended):

```powershell
.\Publish.ps1 -Force
```

### Combined Options

```powershell
.\Publish.ps1 -SkipTests -Force
```

## Versioning

The package version is determined by **Nerdbank.GitVersioning** based on:
- The `version.json` file in the repository root
- Git tags and commit history

### To Create a New Version

1. **Update version.json** (if needed):
   ```json
   {
     "version": "1.2.0",
     "publicReleaseRefSpec": [
       "^refs/heads/main$"
     ]
   }
   ```

2. **Commit the changes**:
   ```powershell
   git add version.json
   git commit -m "Bump version to 1.2.0"
   ```

3. **Tag the release** (optional but recommended):
   ```powershell
   git tag v1.2.0
   git push origin v1.2.0
   ```

4. **Run the publish script**:
   ```powershell
   .\Publish.ps1
   ```

## What the Publish Script Does

1. ? **Git Status Check**: Verifies working directory is clean
2. ? **API Key Validation**: Ensures nuget-key.txt exists and is not empty
3. ? **Clean**: Removes previous build artifacts
4. ? **Restore**: Restores NuGet dependencies
5. ? **Build**: Builds the solution in Release configuration
6. ? **Test**: Runs unit tests (can be skipped with `-SkipTests`)
7. ? **Pack**: Creates the NuGet package in the `artifacts` folder
8. ? **Confirmation**: Prompts you to confirm before publishing
9. ? **Publish**: Uploads the package to NuGet.org
10. ? **Summary**: Shows publish status and package details

## Troubleshooting

### "Package already exists"

If you see an error that the package version already exists:
- The version has already been published to NuGet
- Update the version in `version.json` and commit
- Re-run the publish script

### "Git working directory is not clean"

Options:
1. **Recommended**: Commit or stash your changes first
2. Use `-Force` flag to publish anyway (not recommended)

### "Tests failed"

Options:
1. **Recommended**: Fix the failing tests
2. Use `-SkipTests` to publish without running tests (not recommended)

### "API key invalid"

- Verify your API key is correct in `nuget-key.txt`
- Check the key hasn't expired: https://www.nuget.org/account/apikeys
- Ensure the key has "Push" permissions

### Package not appearing on NuGet.org

- It can take **5-10 minutes** for the package to be indexed
- Check the package URL: https://www.nuget.org/packages/Codacy.Api/
- Verify the publish actually succeeded in the script output

## Security Best Practices

1. ? **Never commit `nuget-key.txt`** - It's already in `.gitignore`
2. ? **Use package-specific API keys** - Don't use a global key
3. ? **Set key expiration** - Configure keys to expire after a reasonable time
4. ? **Rotate keys regularly** - Update your API key periodically
5. ? **Limit key scope** - Only grant "Push" permission, not "Unlist"

## Manual Publishing (Alternative)

If you prefer to publish manually without the script:

```powershell
# Clean and build
dotnet clean --configuration Release
dotnet restore
dotnet build --configuration Release

# Run tests
dotnet test --configuration Release --no-build --filter "Category!=Integration"

# Create package
dotnet pack Codacy.Api\Codacy.Api.csproj --configuration Release --output artifacts

# Publish
dotnet nuget push artifacts\Codacy.Api.*.nupkg --api-key YOUR_KEY --source https://api.nuget.org/v3/index.json
```

## Support

For issues with publishing:
- Check the [NuGet Package Manager logs](https://docs.microsoft.com/en-us/nuget/consume-packages/managing-the-global-packages-and-cache-folders)
- Review [NuGet Publishing Guide](https://docs.microsoft.com/en-us/nuget/nuget-org/publish-a-package)
- Contact the maintainers via GitHub Issues
