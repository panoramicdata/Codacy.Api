# Test Data Requirements

This document outlines the test data requirements for running the Codacy.Api integration test suite.

## Overview

The integration tests require a properly configured Codacy organization with repositories, users, and analysis data. This document details all the necessary test data and how to set it up.

## Required Test Data

### 1. Codacy Organization

**Name**: `panoramicdata` (or your organization name)

**Requirements**:
- Active Codacy account
- API access enabled
- At least 1 active user (you)
- Permissions to create repositories and configure settings

### 2. Test Repository

**Name**: `Codacy.Api.TestFixtures` (recommended)

**Requirements**:
- Git repository hosted on GitHub/GitLab/Bitbucket
- Added to Codacy for analysis
- At least 3 branches (main, develop, feature/test)
- Minimum 10 code files (.cs, .js, .py, etc.)
- Analysis completed (not pending)
- At least 1 commit analyzed

**File Requirements**:
- `.cs` files (for C# analysis)
- `.js` files (for JavaScript analysis)  
- `.py` files (for Python analysis)
- README.md
- .gitignore

**Recommended Structure**:
```
Codacy.Api.TestFixtures/
??? src/
?   ??? CSharp/
?   ?   ??? Sample1.cs
?   ?   ??? Sample2.cs
?   ?   ??? Sample3.cs
?   ??? JavaScript/
?   ?   ??? sample1.js
?   ?   ??? sample2.js
?   ?   ??? sample3.js
?   ??? Python/
?       ??? sample1.py
?       ??? sample2.py
?       ??? sample3.py
??? tests/
?   ??? sample.test.js
??? .gitignore
??? README.md
```

### 3. Branches

**Required Branches**:
1. `main` - Default branch with analyzed code
2. `develop` - Development branch with analyzed code
3. `feature/test` - Feature branch with analyzed code

**Branch Requirements**:
- All branches must be analyzed by Codacy
- Each branch should have at least 1 unique commit
- Branches should have different code (for testing branch-specific data)

### 4. Commits

**Requirements**:
- At least 5 commits total across all branches
- Commits should have different authors (if possible)
- At least 1 commit must be fully analyzed by Codacy
- Commit SHA must be available and valid

**Commit Data Needed**:
- Valid commit SHA for testing (e.g., `abc123def456...`)
- Author email
- Author name
- Timestamp

### 5. Pull Requests

**Requirements**:
- At least 1 open pull request
- At least 1 merged pull request
- Pull requests should have Codacy analysis results
- Pull request numbers should be sequential (1, 2, 3...)

### 6. Issues/Code Quality

**Requirements**:
- Repository should have code quality issues detected by Codacy
- Issues should span multiple categories (Code Style, Error Prone, Security)
- Issues should span multiple severity levels (Critical, High, Medium, Low)
- At least 10 total issues
- At least 1 ignored issue

### 7. Coverage Data

**Requirements**:
- At least 1 coverage report uploaded to Codacy
- Coverage data should be for a specific commit
- Coverage percentage should be > 0%

**Setup**:
```bash
# Upload coverage report (example)
bash <(curl -Ls https://coverage.codacy.com/get.sh) report \
  -r coverage/lcov.info \
  --commit-uuid <commit-sha>
```

### 8. Analysis Tools

**Required Tools Configured**:
- ESLint (for JavaScript)
- Pylint (for Python)
- Roslyn (for C#)
- At least 1 tool must be enabled and have run

### 9. Coding Standards

**Requirements**:
- At least 1 coding standard created in the organization
- Coding standard should have tools configured
- Coding standard should have patterns enabled
- Coding standard should be applied to the test repository

**Setup in Codacy UI**:
1. Go to Organization Settings ? Coding Standards
2. Create new standard: "Test Standard"
3. Configure tools (ESLint, Pylint, etc.)
4. Enable patterns
5. Apply to test repository

### 10. People/Team Members

**Requirements**:
- At least 2 users in the organization (including you)
- Users should have different roles (Admin, Developer)
- Users should have commit activity in the test repository

### 11. Security Scanning

**Requirements**:
- Security scanning enabled on the organization
- Test repository should have security findings (optional)
- At least 1 security category should have findings

**Enable Security**:
1. Go to Organization Settings ? Security
2. Enable security scanning
3. Configure security managers (if needed)

---

## Configuration File Structure

Create or update your `secrets.json` file:

```json
{
  "CodacyApi": {
    "ApiToken": "your-api-token-here",
    "BaseUrl": "https://app.codacy.com",
    "TestOrganization": "panoramicdata",
    "TestProvider": "gh",
    "TestRepository": "Codacy.Api.TestFixtures",
    "TestBranch": "main",
    "TestCommitSha": "abc123def456...",
    "TestPullRequestNumber": 1,
    "TestUserId": 12345,
    "TestCodingStandardId": 67890
  }
}
```

---

## Setup Instructions

### Step 1: Create Test Repository

```bash
# Clone template or create new repository
git clone https://github.com/panoramicdata/Codacy.Api.TestFixtures.git
cd Codacy.Api.TestFixtures

# Add sample code files
mkdir -p src/CSharp src/JavaScript src/Python

# Create sample C# file
cat > src/CSharp/Sample1.cs << 'EOF'
using System;

namespace TestFixtures
{
    public class Sample1
    {
        public void Method1()
        {
            // TODO: Implement this method
            var unused = "This will trigger a code issue";
        }
    }
}
EOF

# Create sample JavaScript file
cat > src/JavaScript/sample1.js << 'EOF'
function sampleFunction() {
    var unusedVariable = "This will trigger a code issue";
    console.log("Hello World");
}
EOF

# Create sample Python file
cat > src/Python/sample1.py << 'EOF'
def sample_function():
    unused_variable = "This will trigger a code issue"
    print("Hello World")
EOF

# Commit and push
git add .
git commit -m "Add test fixtures"
git push origin main

# Create additional branches
git checkout -b develop
echo "# Develop branch" >> README.md
git commit -am "Update README on develop"
git push origin develop

git checkout -b feature/test
echo "# Feature branch" >> README.md
git commit -am "Update README on feature"
git push origin feature/test
```

### Step 2: Add Repository to Codacy

1. Log into Codacy: https://app.codacy.com
2. Click "Add Repository"
3. Select your test repository
4. Wait for initial analysis to complete
5. Verify analysis results appear

### Step 3: Configure Analysis Tools

1. Go to repository settings in Codacy
2. Navigate to "Code Patterns"
3. Enable desired tools (ESLint, Pylint, Roslyn)
4. Configure pattern levels
5. Save settings
6. Trigger re-analysis if needed

### Step 4: Create Pull Request

```bash
git checkout -b test-pr
echo "Test PR content" >> test.txt
git add test.txt
git commit -m "Test PR"
git push origin test-pr

# Create PR on GitHub/GitLab/Bitbucket
# Wait for Codacy to analyze the PR
```

### Step 5: Upload Coverage Report (Optional)

```bash
# Generate coverage report
dotnet test --collect:"XPlat Code Coverage"

# Upload to Codacy
export CODACY_PROJECT_TOKEN="your-project-token"
bash <(curl -Ls https://coverage.codacy.com/get.sh) report \
  -r coverage/coverage.cobertura.xml
```

### Step 6: Create Coding Standard

1. Go to Organization Settings ? Coding Standards
2. Click "Create Coding Standard"
3. Name: "Test Standard"
4. Add tools and configure patterns
5. Apply to test repository

### Step 7: Verify Test Data

Run this verification script:

```powershell
# Verify-TestData.ps1
$config = Get-Content "secrets.json" | ConvertFrom-Json
$apiToken = $config.CodacyApi.ApiToken
$org = $config.CodacyApi.TestOrganization
$provider = $config.CodacyApi.TestProvider
$repo = $config.CodacyApi.TestRepository

Write-Host "Verifying test data for: $org/$repo"

# Test API connectivity
$headers = @{ "api-token" = $apiToken }
$response = Invoke-RestMethod -Uri "https://app.codacy.com/api/v3/version" -Headers $headers
Write-Host "? API Connection: $($response.data)"

# Test repository exists
$response = Invoke-RestMethod -Uri "https://app.codacy.com/api/v3/repositories/$provider/$org/$repo" -Headers $headers
Write-Host "? Repository Found: $($response.data.name)"

# Test branches exist
$response = Invoke-RestMethod -Uri "https://app.codacy.com/api/v3/analysis/organizations/$provider/$org/repositories/$repo/branches" -Headers $headers
Write-Host "? Branches Found: $($response.data.Count)"

Write-Host "`nTest data verification complete!"
```

---

## Maintenance

### Regular Updates

- **Weekly**: Verify all test data is still valid
- **Monthly**: Update commit SHAs and PR numbers
- **Quarterly**: Review and update coding standards

### Data Cleanup

- Remove old test pull requests
- Archive old test branches
- Clean up test user accounts (if applicable)

---

## Troubleshooting

### Repository Not Found
- Verify repository is added to Codacy
- Check organization and provider are correct
- Ensure API token has access to the organization

### No Analysis Data
- Wait for Codacy to complete initial analysis (can take 5-10 minutes)
- Check repository has supported file types
- Verify analysis tools are enabled

### No Issues Found
- Add intentional code issues to trigger detection
- Ensure coding patterns are enabled
- Lower severity thresholds if needed

### No Coverage Data
- Upload a coverage report manually
- Verify coverage format is supported
- Check coverage percentage is > 0%

---

## References

- [Codacy API Documentation](https://docs.codacy.com/codacy-api/)
- [Adding Repositories to Codacy](https://docs.codacy.com/organizations/managing-repositories/)
- [Configuring Code Patterns](https://docs.codacy.com/repositories/configuring-code-patterns/)
- [Coverage Reports](https://docs.codacy.com/coverage-reporter/)

---

**Last Updated**: 2025-11-18  
**Maintainer**: Development Team
