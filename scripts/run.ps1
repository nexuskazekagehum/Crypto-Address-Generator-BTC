$ErrorActionPreference = "Stop"
$project = Join-Path $PSScriptRoot "..\src\Crypto-Address-Generator-BTC\Crypto-Address-Generator-BTC.csproj"
dotnet run --project $project -- @args
