<#
.SYNOPSIS
    Publishes the Codacy.Api NuGet package to nuget.org

.DESCRIPTION
    This script builds, packs, and publishes the Codacy.Api library to NuGet.
    It includes safety checks for git status and runs tests by default.

.PARAMETER SkipTests
    Skip running tests before publishing

.PARAMETER Force
    Force publish even if git working directory is not clean

.EXAMPLE
    .\Publish.ps1
    Builds, tests, and publishes the package

.EXAMPLE
    .\Publish.ps1 -SkipTests
    Builds and publishes without running tests

.EXAMPLE
    .\Publish.ps1 -Force
    Publishes even with uncommitted changes
#>

[CmdletBinding()]
param(
    [Parameter(Mandatory = $false)]
    [switch]$SkipTests,
    
    [Parameter(Mandatory = $false)]
    [switch]$Force
)

$ErrorActionPreference = "Stop"

# Script configuration
$projectPath = "Codacy.Api\Codacy.Api.csproj"
$nugetKeyFile = "nuget-key.txt"
$packOutputDir = "artifacts"

# ANSI color codes for better output
$ColorReset = "`e[0m"
$ColorGreen = "`e[32m"
$ColorYellow = "`e[33m"
$ColorRed = "`e[31m"
$ColorBlue = "`e[34m"
$ColorCyan = "`e[36m"

function Write-Header {
    param([string]$Message)
    Write-Host ""
    Write-Host "${ColorCyan}???????????????????????????????????????????????????????????????????${ColorReset}"
    Write-Host "${ColorCyan}? ${ColorBlue}${Message}${ColorReset}"
    Write-Host "${ColorCyan}???????????????????????????????????????????????????????????????????${ColorReset}"
}

function Write-Success {
    param([string]$Message)
    Write-Host "${ColorGreen}? ${Message}${ColorReset}"
}

function Write-Warning {
    param([string]$Message)
    Write-Host "${ColorYellow}? ${Message}${ColorReset}"
}

function Write-Error {
    param([string]$Message)
    Write-Host "${ColorRed}? ${Message}${ColorReset}"
}

function Write-Info {
    param([string]$Message)
    Write-Host "${ColorBlue}? ${Message}${ColorReset}"
}

# Check if running from solution root
if (-not (Test-Path $projectPath)) {
    Write-Error "Project file not found: $projectPath"
    Write-Info "Please run this script from the solution root directory"
    exit 1
}

Write-Header "Codacy.Api NuGet Package Publisher"

# Step 1: Check git status
Write-Header "Checking Git Status"

try {
    $gitStatus = git status --porcelain 2>&1
    
    if ($LASTEXITCODE -ne 0) {
        Write-Warning "Git is not available or this is not a git repository"
        if (-not $Force) {
            Write-Error "Use -Force to publish anyway"
            exit 1
        }
    }
    elseif ($gitStatus) {
        Write-Warning "Git working directory is not clean:"
        Write-Host $gitStatus
        
        if (-not $Force) {
            Write-Error "Please commit or stash your changes before publishing"
            Write-Info "Use -Force to publish anyway"
            exit 1
        }
        else {
            Write-Warning "Continuing with dirty working directory due to -Force flag"
        }
    }
    else {
        Write-Success "Git working directory is clean"
    }
    
    # Show current branch and latest commit
    $currentBranch = git branch --show-current 2>$null
    $latestCommit = git log -1 --oneline 2>$null
    
    if ($currentBranch) {
        Write-Info "Current branch: $currentBranch"
    }
    if ($latestCommit) {
        Write-Info "Latest commit: $latestCommit"
    }
}
catch {
    Write-Warning "Failed to check git status: $_"
    if (-not $Force) {
        Write-Error "Use -Force to publish anyway"
        exit 1
    }
}

# Step 2: Read NuGet API key
Write-Header "Reading NuGet API Key"

if (-not (Test-Path $nugetKeyFile)) {
    Write-Error "NuGet API key file not found: $nugetKeyFile"
    Write-Info "Please create '$nugetKeyFile' with your NuGet API key"
    Write-Info "Get your API key from: https://www.nuget.org/account/apikeys"
    exit 1
}

$nugetApiKey = Get-Content $nugetKeyFile -Raw
$nugetApiKey = $nugetApiKey.Trim()

if ([string]::IsNullOrWhiteSpace($nugetApiKey)) {
    Write-Error "NuGet API key file is empty: $nugetKeyFile"
    exit 1
}

Write-Success "NuGet API key loaded (${nugetApiKey.Length} characters)"

# Step 3: Clean previous builds
Write-Header "Cleaning Previous Builds"

if (Test-Path $packOutputDir) {
    Remove-Item -Path $packOutputDir -Recurse -Force
    Write-Success "Removed previous artifacts"
}

Write-Info "Running dotnet clean..."
dotnet clean --configuration Release --nologo

if ($LASTEXITCODE -ne 0) {
    Write-Error "Clean failed"
    exit 1
}

Write-Success "Clean completed"

# Step 4: Restore dependencies
Write-Header "Restoring Dependencies"

dotnet restore --nologo

if ($LASTEXITCODE -ne 0) {
    Write-Error "Restore failed"
    exit 1
}

Write-Success "Dependencies restored"

# Step 5: Build solution
Write-Header "Building Solution"

dotnet build --configuration Release --no-restore --nologo

if ($LASTEXITCODE -ne 0) {
    Write-Error "Build failed"
    exit 1
}

Write-Success "Build completed successfully"

# Step 6: Run tests (unless skipped)
if (-not $SkipTests) {
    Write-Header "Running Tests"
    
    Write-Info "Running unit tests only (skipping integration tests)..."
    $testResult = dotnet test --configuration Release --no-build --nologo --filter "Category!=Integration" 2>&1
    
    Write-Host $testResult
    
    if ($LASTEXITCODE -ne 0) {
        Write-Error "Tests failed"
        Write-Info "Fix the failing tests or use -SkipTests to publish anyway"
        exit 1
    }
    
    Write-Success "All tests passed"
}
else {
    Write-Warning "Tests skipped due to -SkipTests flag"
}

# Step 7: Pack the project
Write-Header "Creating NuGet Package"

# Create output directory
New-Item -ItemType Directory -Path $packOutputDir -Force | Out-Null

dotnet pack $projectPath `
    --configuration Release `
    --no-build `
    --output $packOutputDir `
    --nologo

if ($LASTEXITCODE -ne 0) {
    Write-Error "Pack failed"
    exit 1
}

# Find the generated package
$packageFiles = Get-ChildItem -Path $packOutputDir -Filter "*.nupkg" | Where-Object { $_.Name -notlike "*.symbols.nupkg" }

if ($packageFiles.Count -eq 0) {
    Write-Error "No package file found in $packOutputDir"
    exit 1
}

$packageFile = $packageFiles[0]
Write-Success "Package created: $($packageFile.Name)"
Write-Info "Package size: $([math]::Round($packageFile.Length / 1KB, 2)) KB"

# Extract version from package name
if ($packageFile.Name -match 'Codacy\.Api\.(\d+\.\d+\.\d+.*?)\.nupkg') {
    $packageVersion = $Matches[1]
    Write-Info "Package version: $packageVersion"
}

# Step 8: Publish to NuGet
Write-Header "Publishing to NuGet.org"

Write-Warning "This will publish the package to NuGet.org"
Write-Host "Package: $($packageFile.Name)"
Write-Host ""

$confirmation = Read-Host "Do you want to continue? (yes/no)"

if ($confirmation -ne "yes" -and $confirmation -ne "y") {
    Write-Warning "Publish cancelled by user"
    exit 0
}

Write-Info "Publishing package..."

dotnet nuget push $packageFile.FullName `
    --api-key $nugetApiKey `
    --source https://api.nuget.org/v3/index.json `
    --skip-duplicate `
    --nologo

if ($LASTEXITCODE -ne 0) {
    Write-Error "Publish failed"
    Write-Info "The package file is still available in: $packOutputDir"
    exit 1
}

Write-Success "Package published successfully!"

# Step 9: Summary
Write-Header "Publish Summary"

Write-Success "Package: $($packageFile.Name)"
if ($packageVersion) {
    Write-Success "Version: $packageVersion"
}
Write-Success "Published to: https://www.nuget.org/packages/Codacy.Api/"
Write-Info "It may take a few minutes for the package to be indexed and appear in search"

if ($SkipTests) {
    Write-Warning "Tests were skipped - ensure you've tested the package before publishing!"
}

Write-Host ""
Write-Host "${ColorGreen}???????????????????????????????????????????????????????????????????${ColorReset}"
Write-Host "${ColorGreen}? ${ColorGreen}? Publish completed successfully!${ColorReset}"
Write-Host "${ColorGreen}???????????????????????????????????????????????????????????????????${ColorReset}"
Write-Host ""
