# Usage Guide

## Running Crypto-Address-Generator-BTC

```bash
dotnet run --project src/Crypto-Address-Generator-BTC/Crypto-Address-Generator-BTC.csproj
```

## CLI Arguments

| Argument | Description |
|----------|-------------|
| `--config` | Path to a custom appsettings file. |
| `--verbose` | Enable verbose logging. |

## Sample Data

The `data/samples.json` file contains realistic-looking simulated data for local testing.

## Extending

Add new providers by implementing the domain interfaces in `Core/Services` and registering them in `Program.cs`.
