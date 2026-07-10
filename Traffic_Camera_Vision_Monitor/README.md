# Traffic Camera Vision Monitor

A console app that uses a vision-capable LLM to analyze traffic camera snapshots, extract structured traffic data, and raise alerts for anomalies — built on `Microsoft.Extensions.AI`.

## How it works

For each image in `traffic-cam/`, the app sends it to the model along with the camera name and asks it to extract structured output: traffic status (`Clear`/`Flowing`/`Congested`/`Blocked`), and counts of cars and trucks.

The model is also given a `RaiseAlert` tool it can call if a camera appears broken or something unusual/dangerous is happening — not just for heavy traffic. Responses are cached via Redis (`UseDistributedCache()`) so re-analyzing the same image doesn't re-call the model.

## Running locally

Start Redis:

```
docker run -p 6379:6379 -d redis
```

Set your AI endpoint and key via user secrets:

```
dotnet user-secrets set "AI:Endpoint" "https://SOMETHING.openai.azure.com/"
dotnet user-secrets set "AI:Key" "YourKeyHere"
```

Then run (use a vision-capable model, e.g. `gpt-4o-mini`):

```
dotnet run
```
