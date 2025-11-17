# Setting Up User Secrets for Testing

This guide explains how to configure user secrets for running integration tests against the Codacy API.

## Prerequisites

- A Codacy account (free or paid)
- An API token from Codacy

## Getting a Codacy API Token

1. Log into your Codacy account at https://app.codacy.com
2. Navigate to **Account Settings** → **Access Management** → **API Tokens**
3. Click **Create API Token**
4. Give your token a name (e.g., "Local Development")
5. Select the appropriate permissions (for testing, you'll need read access at minimum)
6. Copy the generated token (it will only be shown once!)

## Configuring User Secrets

### Option 1: Using the .NET CLI (Recommended)

```bash
cd Codacy.Api.Test

# Initialize user secrets (if not already done)
dotnet user-secrets init

# Set your API token
dotnet user-secrets set "CodacyApi:ApiToken" "your-token-here"

# Optional: Set custom base URL (for self-hosted instances)
dotnet user-secrets set "CodacyApi:BaseUrl" "https://app.codacy.com"

# Optional: Set test organization and repository details
dotnet user-secrets set "CodacyApi:TestOrganization" "your-org-name"
dotnet user-secrets set "CodacyApi:TestProvider" "gh"
dotnet user-secrets set "CodacyApi:TestRepository" "your-repo-name"
```

### Option 2: Manual Configuration

1. Locate your user secrets file:
   - **Windows**: `%APPDATA%\Microsoft\UserSecrets\5c6b597f-752g-5b55-c694-5520ecc675b0\secrets.json`
   - **Linux/Mac**: `~/.microsoft/usersecrets/5c6b597f-752g-5b55-c694-5520ecc675b0/secrets.json`

2. Create or edit the `secrets.json` file with the following structure:

```json
{
  "CodacyApi": {
    "ApiToken": "your-actual-api-token",
    "BaseUrl": "https://app.codacy.com",
    "TestOrganization": "panoramicdata",
    "TestProvider": "gh",
    "TestRepository": "Codacy.Api"
  }
}
```

## Configuration Options

| Setting | Description | Required | Default |
|---------|-------------|----------|---------|
| `ApiToken` | Your Codacy API token | Yes | - |
| `BaseUrl` | Codacy instance URL | No | `https://app.codacy.com` |
| `TestOrganization` | Organization name for tests | No | - |
| `TestProvider` | Git provider (`gh`, `gl`, `bb`) | No | - |
| `TestRepository` | Repository name for tests | No | - |

## Verifying Your Configuration

Run the test suite to verify your configuration:

```bash
dotnet test
```

If configured correctly, all tests should pass.

## Security Notes

- **Never commit your API token** to source control
- User secrets are stored outside the project directory
- The `secrets.example.json` file is a template only and should not contain real credentials
- Tokens can be revoked at any time from the Codacy dashboard

## Troubleshooting

### "API token not configured" Error

This means the `CodacyApi:ApiToken` secret is not set. Follow the configuration steps above.

### Authentication Failed

- Verify your token is correct
- Check that the token hasn't expired
- Ensure the token has the necessary permissions

### Tests Failing

- Verify the organization and repository names are correct
- Check that your token has access to the specified resources
- Ensure you're using the correct provider code (`gh` for GitHub, `gl` for GitLab, `bb` for Bitbucket)

## Example: Complete Setup

```bash
# Navigate to test project
cd C:\Users\david\Projects\Codacy.Api\Codacy.Api.Test

# Initialize user secrets
dotnet user-secrets init

# Set API token
dotnet user-secrets set "CodacyApi:ApiToken" "abc123def456..."

# Set test context
dotnet user-secrets set "CodacyApi:TestOrganization" "panoramicdata"
dotnet user-secrets set "CodacyApi:TestProvider" "gh"
dotnet user-secrets set "CodacyApi:TestRepository" "Codacy.Api"

# Verify secrets are set
dotnet user-secrets list

# Run tests
dotnet test
```

## Additional Resources

- [Codacy API Documentation](https://docs.codacy.com/codacy-api/)
- [Safe Storage of App Secrets in .NET](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets)
