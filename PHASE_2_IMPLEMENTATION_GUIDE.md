# Phase 2 Implementation Guide

## Overview
Phase 2 focuses on fixing Repository and Analysis API integration tests by ensuring the test repository is properly configured in Codacy.

## Current Status
- **Phase 1**: ? COMPLETED (Test infrastructure ready)
- **Phase 2**: IN PROGRESS
  - Setup utilities: ? Created
  - Repository addition: ? Pending manual action
  - Test fixes: ?? Ready to execute

## Prerequisites Check

Before running Phase 2 tests, verify the environment is ready:

```bash
dotnet test --filter "FullyQualifiedName~VerifyPhase2Prerequisites"
```

Expected output should show all checks passing:
- ? Repository Exists
- ? Repository Analyzed
- ? Branches Configured
- ? Files Indexed

## Step-by-Step Phase 2 Execution

### Step 1: Add Repository to Codacy

**Option A: Automated (Recommended)**
```bash
dotnet test --filter "FullyQualifiedName~AddTestRepositoryToCodacy"
```

This test will:
1. Check if repository already exists
2. Add repository via Codacy API if not found
3. Wait for initial analysis (up to 5 minutes)
4. Display final environment status

**Option B: Manual via Codacy UI**
1. Go to https://app.codacy.com
2. Login and select `panoramicdata` organization
3. Click "Add repository"
4. Search for `Codacy.Api.TestRepo`
5. Click "Add repository"
6. Wait for analysis to complete (~5 minutes)

### Step 2: Verify Environment

```bash
dotnet test --filter "FullyQualifiedName~VerifyPhase2Prerequisites"
```

This will check all prerequisites and provide specific guidance if anything is missing.

### Step 3: List Available Repositories (Optional)

```bash
dotnet test --filter "FullyQualifiedName~ListAvailableRepositories"
```

This shows all repositories in the organization and confirms the test repo is present.

### Step 4: Run Repository API Tests

Once environment is ready, run the failing P1 tests:

```bash
# Run all Repository API tests
dotnet test --filter "FullyQualifiedName~RepositoriesApiTests"

# Or run individual tests
dotnet test --filter "FullyQualifiedName~ListRepositoryBranches_ReturnsBranches"
dotnet test --filter "FullyQualifiedName~ListRepositoryBranches_WithPagination"
dotnet test --filter "FullyQualifiedName~ListFiles_ReturnsFiles"
dotnet test --filter "FullyQualifiedName~ListFiles_WithSearch_FiltersResults"
```

Expected results:
- ? All 4 P1 Repository tests passing
- ? Total Repository API: 9/9 (100%)

### Step 5: Run Analysis API Tests

```bash
# Run all Analysis API tests
dotnet test --filter "FullyQualifiedName~AnalysisApiTests"

# Or run individual failing tests
dotnet test --filter "FullyQualifiedName~ListRepositoryTools_ReturnsTools"
dotnet test --filter "FullyQualifiedName~ListCategoryOverviews_ReturnsCategories"
```

Expected results:
- ? All 2 P2 Analysis tests passing
- ? Total Analysis API: 12/12 (100%)

## Test Files Created

### Phase2SetupTests.cs
Located: `Codacy.Api.Test/Integration/Phase2SetupTests.cs`

**Tests included**:
1. `AddTestRepositoryToCodacy_AddsRepository_Successfully`
   - Adds repository to Codacy via API
   - Waits for initial analysis
   - Shows environment status

2. `VerifyPhase2Prerequisites_ChecksAllRequirements`
   - Comprehensive prerequisite check
   - Provides specific guidance for failures
   - Must pass before running Phase 2 tests

3. `ListAvailableRepositories_ShowsRepositoriesInOrganization`
   - Lists all org repositories
   - Highlights test repository
   - Useful for verification

4. `RemoveTestRepositoryFromCodacy_RemovesRepository_Successfully`
   - Cleanup test (skipped by default)
   - Only run manually when needed

## Expected Test Results After Phase 2

### Before Phase 2
```
Integration Tests: 59/72 (82%)
??? Repository API: 6/9 (67%)  ? 3 failing
??? Analysis API: 10/12 (83%)  ? 2 failing
```

### After Phase 2
```
Integration Tests: 65/72 (90%)  ? +6 tests fixed
??? Repository API: 9/9 (100%) ? ? COMPLETE
??? Analysis API: 12/12 (100%) ? ? COMPLETE
```

## Troubleshooting

### Repository Not Found (404)
**Symptoms**: Tests fail with "Repository not found in Codacy"

**Solution**:
```bash
# Check if repository is added
dotnet test --filter "FullyQualifiedName~VerifyPhase2Prerequisites"

# Add repository if missing
dotnet test --filter "FullyQualifiedName~AddTestRepositoryToCodacy"
```

### Analysis Not Complete
**Symptoms**: Repository exists but no files/branches found

**Solution**:
1. Wait 5-10 minutes for analysis to complete
2. Check Codacy UI: https://app.codacy.com/gh/panoramicdata/Codacy.Api.TestRepo
3. Verify repository has commits
4. Check for analysis errors in Codacy UI

### API Rate Limiting
**Symptoms**: Tests fail with 429 Too Many Requests

**Solution**:
- TestDataManager automatically retries with exponential backoff
- Wait a few minutes between test runs
- Check rate limit status in Codacy account settings

### Branches Not Showing
**Symptoms**: `HasBranches: False` in environment check

**Solution**:
1. Verify branches exist in GitHub: https://github.com/panoramicdata/Codacy.Api.TestRepo
2. Wait for Codacy to sync branches (~1 minute after analysis)
3. Trigger re-analysis if needed: `POST /api/v3/repositories/{provider}/{org}/{repo}/commits/{sha}/reanalyze`

## Files Modified/Created in Phase 2

### Created
- ? `Codacy.Api.Test/Integration/Phase2SetupTests.cs` (200+ lines)
- ? `PHASE_2_SETUP_GUIDE.md` (Setup instructions)
- ? `PHASE_2_IMPLEMENTATION_GUIDE.md` (This file)

### Modified
- None (all changes are new test files)

## Success Criteria

Phase 2 is complete when:

- [ ] Repository added to Codacy successfully
- [ ] Repository fully analyzed (files, branches, issues indexed)
- [ ] All 4 P1 Repository API tests passing
- [ ] All 2 P2 Analysis API tests passing
- [ ] Total: 6 tests fixed (from 59/72 to 65/72)
- [ ] Environment verification test passes
- [ ] No regressions in existing tests

## Next Steps After Phase 2

Once Phase 2 is complete (65/72 tests passing), proceed to:

**Phase 3: People, Coding Standards & Security Coverage**
- Fix People API tests (2 tests)
- Fix Coding Standards API tests (4 tests)
- Fix Security API tests (1 test)

See `MASTER_PLAN.md` for full Phase 3 details.

## Quick Reference Commands

```bash
# Full Phase 2 workflow
dotnet test --filter "FullyQualifiedName~AddTestRepositoryToCodacy"
dotnet test --filter "FullyQualifiedName~VerifyPhase2Prerequisites"
dotnet test --filter "FullyQualifiedName~RepositoriesApiTests"
dotnet test --filter "FullyQualifiedName~AnalysisApiTests"

# Check environment status
dotnet test --filter "FullyQualifiedName~GetEnvironmentStatus"

# List org repositories
dotnet test --filter "FullyQualifiedName~ListAvailableRepositories"

# Run all Phase 2 setup tests
dotnet test --filter "Category=Setup"
```

## Timeline

| Task | Duration | Status |
|------|----------|--------|
| Create setup tests | 30 min | ? Complete |
| Add repository to Codacy | 1 min | ? Pending |
| Wait for analysis | 5-10 min | ? Pending |
| Fix Repository API tests | 5 min | ?? Ready |
| Fix Analysis API tests | 5 min | ?? Ready |
| Verify all tests | 5 min | ?? Ready |
| **Total** | **~30 min** | **In Progress** |

---

**Status**: Phase 2 Setup Complete - Ready for Execution  
**Action Required**: Add repository to Codacy (automated or manual)  
**Next Test to Run**: `dotnet test --filter "FullyQualifiedName~AddTestRepositoryToCodacy"`

