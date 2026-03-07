param(
  [Parameter(Mandatory = $true, Position = 0)]
  [string]$InputPath,

  [Parameter(Mandatory = $false, Position = 1)]
  [string]$OutputDir = "output/local-skill",

  [Parameter(ValueFromRemainingArguments = $true)]
  [string[]]$ExtraArgs
)

$ErrorActionPreference = "Stop"

$ScriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$ArtifactHome = Resolve-Path (Join-Path $ScriptDir "..\..\..")
$LaunchDir = (Get-Location).Path

function Resolve-FromLaunchDir {
  param([Parameter(Mandatory = $true)][string]$PathValue)

  if ([System.IO.Path]::IsPathRooted($PathValue)) {
    return [System.IO.Path]::GetFullPath($PathValue)
  }

  return [System.IO.Path]::GetFullPath((Join-Path $LaunchDir $PathValue))
}

$InputPath = Resolve-FromLaunchDir $InputPath
$OutputDir = Resolve-FromLaunchDir $OutputDir

# Ensure dotnet is resolvable on common Windows installs.
$dotnetPath = "C:\Program Files\dotnet"
if (Test-Path $dotnetPath) {
  $env:PATH = "$dotnetPath;$env:PATH"
}

if (-not $env:DOTNET_ROLL_FORWARD) {
  $env:DOTNET_ROLL_FORWARD = "Major"
}

Push-Location $ArtifactHome
try {
  if (-not (Test-Path $InputPath)) {
    throw "[error] input path not found: $InputPath"
  }

  New-Item -ItemType Directory -Force -Path $OutputDir | Out-Null

  Write-Host "[info] launch dir    : $LaunchDir"
  Write-Host "[info] artifact home : $ArtifactHome"
  Write-Host "[info] input         : $InputPath"
  Write-Host "[info] output        : $OutputDir"

  $argsList = @(
    "run",
    "--project", "src/AssemblyInspector.Cli",
    "--no-build",
    "-c", "Release",
    "--",
    $InputPath,
    $OutputDir
  )

  if ($ExtraArgs) {
    $argsList += $ExtraArgs
  }

  & dotnet @argsList

  if ($LASTEXITCODE -ne 0) {
    throw "dotnet run failed with exit code $LASTEXITCODE"
  }

  Write-Host "[ok] inspection complete"
}
finally {
  Pop-Location
}
