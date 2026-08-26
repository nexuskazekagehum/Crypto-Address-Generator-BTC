# Frequently Asked Questions

## Is this project connected to real funds or blockchains?

No. Crypto-Address-Generator-BTC is an educational simulation and does not perform real transactions.

## Can I use this in production?

No. This is a lab tool intended for learning, CTF exercises, and authorized research.

## How do I add a new provider?

Implement the relevant interface in `Core/Services` and register it in `Program.cs`.

## Where are secrets stored?

Secrets should be supplied via environment variables only; never commit them to source control.

## How do I run tests?

```bash
dotnet test Crypto-Address-Generator-BTC.sln
```
