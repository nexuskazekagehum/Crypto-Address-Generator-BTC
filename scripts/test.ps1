$ErrorActionPreference = "Stop"
$sln = Join-Path $PSScriptRoot "..\Crypto-Address-Generator-BTC.sln"
dotnet test $sln --configuration Release --verbosity normal
