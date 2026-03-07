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
$CliDll = Join-Path $ScriptDir "..\bin\net8.0\AssemblyInspector.Cli.dll"
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

if (-not (Test-Path $CliDll)) {
  throw "[error] bundled CLI not found: $CliDll`n[hint] republish into skills/dotnet-assembly-inspector-local/bin/net8.0"
}

if (-not (Test-Path $InputPath)) {
  throw "[error] input path not found: $InputPath"
}

New-Item -ItemType Directory -Force -Path $OutputDir | Out-Null

Write-Host "[info] launch dir    : $LaunchDir"
Write-Host "[info] cli dll       : $CliDll"
Write-Host "[info] input         : $InputPath"
Write-Host "[info] output        : $OutputDir"

$argsList = @(
  $CliDll,
  $InputPath,
  $OutputDir
)

if ($ExtraArgs) {
  $argsList += $ExtraArgs
}

& dotnet @argsList

if ($LASTEXITCODE -ne 0) {
  throw "dotnet failed with exit code $LASTEXITCODE"
}

Write-Host "[ok] inspection complete"
