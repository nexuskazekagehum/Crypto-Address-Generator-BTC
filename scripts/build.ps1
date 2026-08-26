param(
    [ValidateSet("Debug", "Release")]
    [string]$Configuration = "Debug"
)
$ErrorActionPreference = "Stop"
$sln = Join-Path $PSScriptRoot "..\Crypto-Address-Generator-BTC.sln"
dotnet restore $sln
dotnet build $sln --configuration $Configuration
if ($LASTEXITCODE -ne 0) { throw "Build failed" }
dotnet test $sln --configuration $Configuration
