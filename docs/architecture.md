# Architecture

Crypto-Address-Generator-BTC is a modular console module for address generation. It separates concerns into Core, Infrastructure, and entry point layers.

## Layers

```
Program
  |
  +-- CryptoModule
        |
        +-- DataProvider
        +-- Repository
        +-- Configuration
```

## Components

| Component | Responsibility |
|-----------|---------------|
| `ICryptoModule` | Orchestrates simulation and aggregation logic. |
| `IDataProvider` | Fetches simulated address generation data. |
| `IRepository` | In-memory storage of snapshots and results. |
| `IConfigurationLoader` | Loads settings from JSON and environment variables. |
| `ILogger` | Writes structured log output. |
