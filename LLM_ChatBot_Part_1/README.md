# LLM ChatBot — Part 1

A console app demonstrating core `IChatClient` patterns with LLMs, built on `Microsoft.Extensions.AI`.

## How it works

- **BasicCompletion** — a plain non-streaming completion, plus a streaming completion printed token-by-token, with token usage reporting.
- **PropertyListingExtraction** — extracts structured data (listing type, neighbourhood, price, amenities, summary) from free-text real-estate listings using typed structured output.
- **HackerNewsCategorization** — fetches the current top Hacker News stories live and categorizes them by topic using typed structured output.

`Support/UsePromptBasedFunctionCallingStep.cs` is a reusable `IChatClient` decorator that emulates function calling via prompting for models that don't natively support it.

## Running locally

Set your AI endpoint and key via user secrets:

```
dotnet user-secrets set "AI:Endpoint" "https://SOMETHING.openai.azure.com/"
dotnet user-secrets set "AI:Key" "YourKeyHere"
```

Then choose which demo to run by commenting/uncommenting the corresponding line at the bottom of `Program.cs`, and run:

```
dotnet run
```
