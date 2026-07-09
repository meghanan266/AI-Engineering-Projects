# LLM ChatBot — Part 2

Builds on [Part 1](../LLM_ChatBot_Part_1) with tool calling and a custom `IChatClient` middleware pipeline, using `Microsoft.Extensions.AI`.

## How it works

- **ChatBot** — an interactive shopping-assistant chatbot with tool calling: the model can call `GetPrice` and `AddSocksToCart` functions to answer questions and update a cart.
- **UseLanguageStep** (`Support/`) — custom middleware that appends an instruction forcing all replies into a given language.
- **UseRateLimitStep** (`Support/`) — custom middleware that rate-limits outgoing requests using a fixed-window limiter.
- **UsePromptBasedFunctionCallingStep** (`Support/`) — emulates function calling via prompting for models without native tool-calling support.

`Program.cs` wires these into the chat client pipeline: `UseLanguage("Welsh") -> UseRateLimit(...) -> UseFunctionInvocation()`, demonstrating how cross-cutting behavior composes around an `IChatClient`.

Also includes the Part 1 demos (BasicCompletion, PropertyListingExtraction, HackerNewsCategorization) as this is a standalone copy of the same project, extended further.

## Running locally

Set your AI endpoint and key via user secrets:

```
dotnet user-secrets set "AI:Endpoint" "https://SOMETHING.openai.azure.com/"
dotnet user-secrets set "AI:Key" "YourKeyHere"
```

Then choose which demo to run by commenting/uncommenting the corresponding line at the bottom of `Program.cs` (ChatBot is active by default), and run:

```
dotnet run
```
