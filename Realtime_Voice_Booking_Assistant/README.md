# Realtime Voice Booking Assistant

A Blazor Server app for real-time, speech-to-speech conversation with an LLM, built on `Microsoft.Extensions.AI` and the OpenAI Realtime API.

## How it works

The assistant plays the role of a virtual phone receptionist for a cowboy-themed restaurant, "The Wild Brunch." It:

- Streams microphone audio to the model and streams the model's spoken reply back out to the speaker, with barge-in support (interrupting the assistant stops its playback).
- Live-transcribes both sides of the conversation to the screen.
- Uses tool calling mid-conversation to check table availability and book a table, driven entirely by voice.

`Support/OpenAIRealtimeExtensions.cs` bridges the OpenAI Realtime SDK's `RealtimeConversationSession` with `Microsoft.Extensions.AI`'s `AIFunction` tool-calling abstractions.

## Running locally

Set your AI endpoint and key via user secrets:

```
dotnet user-secrets set "AI:Endpoint" "https://SOMETHING.openai.azure.com/"
dotnet user-secrets set "AI:Key" "YourKeyHere"
```

Then run (requires a realtime-capable model, e.g. `gpt-4o-realtime-preview`):

```
dotnet run
```

Open the app in a browser, grant microphone access, and start talking.
