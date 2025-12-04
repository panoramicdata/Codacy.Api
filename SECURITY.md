# Security Guidelines

## Protecting Secrets

This project uses API tokens and secrets that must **never** be committed to the repository.

### Setup

1. **Install the pre-commit hook** to catch accidental secret commits:
   ```bash
   git config core.hooksPath .githooks
   ```

2. **Use User Secrets** for local development (already configured):
   ```bash
   cd Codacy.Api.Test
   dotnet user-secrets set "CodacyApi:ApiToken" "your-token-here"
   ```

3. **Never commit**:
   - `secrets.json` files (use `secrets.example.json` as template)
   - `nuget-key.txt` (for NuGet publishing)
   - Any file containing API tokens or passwords

### What's Protected

The `.gitignore` includes patterns to prevent committing:
- `**/secrets.json` - User secrets
- `*-key.txt` / `*-token.txt` - API keys and tokens
- `*.log` / `*.logs` - Log files that might contain sensitive data
- `**/TestResults/` - Test output that might leak secrets

### If You Accidentally Commit a Secret

1. **Immediately revoke the exposed credential** in the source system
2. **Generate a new credential**
3. **Update your local secrets** with the new value
4. Consider using [BFG Repo-Cleaner](https://rtyley.github.io/bfg-repo-cleaner/) to remove from Git history

### GitGuardian Integration

This repository is monitored by GitGuardian for accidental secret exposure. If you receive an alert:

1. Don't panic - but act quickly
2. Revoke the exposed credential immediately
3. Generate and configure a new credential
4. Review how the exposure happened and update processes

### Debugging Safely

When debugging tests that use API tokens:
- The `LoggingHttpMessageHandler` masks tokens in logs
- However, debugger evaluations can expose raw values
- **Never copy/paste debugger output containing request headers**
- Be careful with exception details that include HTTP request info

## Reporting Security Issues

If you discover a security vulnerability, please report it privately to the repository maintainers rather than opening a public issue.
