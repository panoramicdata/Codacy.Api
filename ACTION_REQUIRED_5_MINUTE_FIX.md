# Action Required - Complete Phase 2 in 5 Minutes

## Current Situation

? **All infrastructure complete**  
? **All tests properly written**  
? **All documentation created**  
? **Root cause identified**

?? **Single blocker**: API token missing `repository:read` permission

## Quick Action Checklist

### ? Step 1: Open Codacy API Tokens Page (30 seconds)
```
https://app.codacy.com/account/api-tokens
```

### ? Step 2: Create New Token (1 minute)
1. Click **"Create API Token"**
2. Name: `Integration Tests - Full Access`
3. **CRITICAL**: Check these scopes:
   - ? Organization: read
   - ? **Repository: read** ? **THIS IS THE KEY ONE**
   - ? Repository: write (optional, but recommended)
4. Click **"Create"**
5. **Copy the token** (you won't see it again)

### ? Step 3: Update User Secrets (1 minute)
1. Open: `C:\Users\david\AppData\Roaming\Microsoft\UserSecrets\5c6b597f-752g-5b55-c694-5520ecc675b0\secrets.json`
2. Replace the `ApiToken` value:
   ```json
   {
     "CodacyApi:TestRepository": "Codacy.Api.TestRepo",
     "CodacyApi:TestProvider": "gh",
     "CodacyApi:TestOrganization": "panoramicdata",
     "CodacyApi:TestCommitSha": "1a25fd0edc2ac3c33130912fcdbace5908cecfbf",
     "CodacyApi:BaseUrl": "https://app.codacy.com",
     "CodacyApi:ApiToken": "PASTE_NEW_TOKEN_HERE",
     "CodacyApi:TestBranch": "main"
   }
   ```
3. Save the file

### ? Step 4: Verify Repository Tests (1 minute)
```bash
dotnet test --filter "FullyQualifiedName~RepositoriesApiTests" --logger "console;verbosity=minimal"
```

**Expected Output**:
```
Passed: 9/9
Total: 9
```

### ? Step 5: Verify All Integration Tests (1 minute)
```bash
dotnet test --filter "Category=Integration" --logger "console;verbosity=minimal"
```

**Expected Output**:
```
Passed: 72/72
Total: 72
```

### ? Step 6: Update Master Plan (1 minute)
1. Mark Phase 2 as **COMPLETE** ?
2. Update status to show 72/72 integration tests passing
3. Begin Phase 3 planning

## Expected Results After Completion

### Before
```
Integration Tests: 59/72 (82%)
  - Passing: 59
  - Failing: 13 (API token permission)
```

### After
```
Integration Tests: 72/72 (100%) ?
  - Passing: 72
  - Failing: 0
```

## Troubleshooting

### If tests still fail after token regeneration:

**Check 1: Token has correct scope**
- Re-check that `repository:read` is enabled
- Try creating a new token if unsure

**Check 2: Token is properly saved**
- Verify `secrets.json` has the new token (no typos)
- Ensure file is saved

**Check 3: Test repository exists**
```bash
dotnet test --filter "FullyQualifiedName~ListAvailableRepositories"
```
Should show `Codacy.Api.TestRepo` in the list

**Check 4: Run diagnostic**
```bash
dotnet test --filter "FullyQualifiedName~DebugRepositoryAccess"
```
Should show successful direct repository access

### If all else fails:
- Check documentation: `CODACY_API_TOKEN_PERMISSION_ISSUE.md`
- Review: `PHASE_2_FINAL_STATUS.md`

## What Happens After This Works

### Immediate
- ? All 72 integration tests pass
- ? Phase 2 marked complete
- ? 100% integration test coverage achieved

### Next Steps (Phase 3)
1. Begin People API test enhancements
2. Create coding standard in Codacy
3. Configure security scanning
4. Run remaining tests

### Long Term
- Phase 4: Account API & polish
- Phase 5: Extended coverage & edge cases
- Final: Publish with "100% Test Coverage" badge

## Quick Reference

**Files to Check**:
- User secrets: `%APPDATA%\Microsoft\UserSecrets\5c6b597f-752g-5b55-c694-5520ecc675b0\secrets.json`
- Master plan: `MASTER_PLAN.md`
- Status: `PHASE_2_FINAL_STATUS.md`

**Commands to Run**:
```bash
# Test repository access
dotnet test --filter "FullyQualifiedName~RepositoriesApiTests"

# Test everything
dotnet test --filter "Category=Integration"

# Debug if needed
dotnet test --filter "FullyQualifiedName~DebugRepositoryAccess"
```

## Time Breakdown

| Step | Time | Cumulative |
|------|------|------------|
| 1. Open Codacy | 30s | 30s |
| 2. Create token | 1m | 1m 30s |
| 3. Update secrets | 1m | 2m 30s |
| 4. Test repos | 1m | 3m 30s |
| 5. Test all | 1m | 4m 30s |
| 6. Update plan | 30s | **5m** |

**Total**: ~5 minutes to complete Phase 2 and achieve 100% integration test coverage!

---

**Ready?** Start with Step 1: https://app.codacy.com/account/api-tokens

