# Adding Test Repository to Codacy - Step-by-Step Guide

## Current Status
? Repository `panoramicdata/Codacy.Api.TestRepo` is NOT in Codacy yet
? This is blocking 6 P1 integration tests

## Prerequisites
- ? Repository exists on GitHub: https://github.com/panoramicdata/Codacy.Api.TestRepo
- ? Codacy account with access to `panoramicdata` organization
- ? API token configured in user secrets

## Steps to Add Repository to Codacy

### Option 1: Add via Codacy Web UI (Recommended)

1. **Login to Codacy**
   - Go to: https://app.codacy.com
   - Login with your GitHub account

2. **Navigate to Organization**
   - Click on organization dropdown (top-left)
   - Select `panoramicdata`

3. **Add Repository**
   - Click "Add repository" or "Repositories" ? "Add repository"
   - Search for `Codacy.Api.TestRepo`
   - Click "Add repository" next to it

4. **Wait for Initial Analysis**
   - Codacy will automatically start analyzing the repository
   - This typically takes 2-5 minutes for a small repository
   - You can monitor progress on the repository dashboard

5. **Verify Setup**
   - Check that branches are visible (main, develop, feature/test)
   - Check that files are listed in the repository
   - Check that analysis results are shown

### Option 2: Add via API (Automated)

I can create a helper script to add the repository programmatically:

```bash
# Run this test to add the repository
dotnet test --filter "FullyQualifiedName~AddTestRepositoryToCodacy"
```

## Post-Addition Checklist

After adding the repository, verify it's ready:

```bash
# Run environment status check
dotnet test --filter "FullyQualifiedName~TestDataManager_GetEnvironmentStatus_ReturnsCompleteStatus"
```

Expected output should show:
- ? Repository Exists: True
- ? Has Analysis Data: True
- ? Has Branches: True
- ? Environment Ready: True

## Timeline

| Step | Duration | Notes |
|------|----------|-------|
| Add repository to Codacy | < 1 minute | Via UI or API |
| Initial analysis | 2-5 minutes | Automatic |
| Branch detection | < 1 minute | Automatic |
| Environment ready | ~5 minutes total | Then tests can run |

## Troubleshooting

### Repository Not Found in Add Dialog
- Ensure GitHub integration is connected
- Verify organization permissions
- Try refreshing the organization

### Analysis Not Starting
- Check repository has commits
- Verify GitHub webhook is configured
- Check Codacy service status: https://status.codacy.com

### Branches Not Showing
- Verify branches exist in GitHub
- Wait for initial analysis to complete
- Refresh repository page

## Next Steps After Repository is Added

Once the repository is added and analyzed:

1. **Re-run Environment Check**
   ```bash
   dotnet test --filter "Category=Example"
   ```

2. **Run Failing Tests**
   ```bash
   # Run all Repository API tests
   dotnet test --filter "FullyQualifiedName~RepositoriesApiTests"
   
   # Run all Analysis API tests
   dotnet test --filter "FullyQualifiedName~AnalysisApiTests"
   ```

3. **Expected Results**
   - All P1 tests should now pass
   - Repository API: 9/9 passing (100%)
   - Analysis API: 12/12 passing (100%)

---

**Action Required**: Add `panoramicdata/Codacy.Api.TestRepo` to Codacy before proceeding with Phase 2 tests.

**Estimated Time**: 5-10 minutes (including analysis)

**Status**: BLOCKED - Waiting for repository to be added to Codacy
