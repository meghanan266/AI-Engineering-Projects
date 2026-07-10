# Agentic RAG Chatbot

An extension of [RAG_Chatbot](../RAG_Chatbot) that adds corrective retrieval and an agentic planning loop, built on `Microsoft.Extensions.AI` and Qdrant. Depends on the same Qdrant `manuals` collection and `data/products.json` as RAG_Chatbot — run that project's Ingestion step first.

## How it works

- **Corrective retrieval** (`ContextRelevancyEvaluator`) — after retrieving the closest manual chunks for a question, an LLM grades each chunk's relevance (Awful/Poor/Good/Perfect). If the average score is too low, retrieval is considered to have failed.
- **Agentic fallback** (`src/Planner`) — on a failed retrieval, a `PlanGenerator` breaks the task into steps, a `PlanExecutor` runs each step (with a live web-search tool available), and a `PlanEvaluator` checks progress after each step and decides whether to keep going, revise the plan, or return a final result. That result is folded back into context as if it were a retrieved chunk.
- **Search** (`Search/`) — `DuckDuckGoSearchTool` (used by default, no API key needed) and `BingSearchTool` (needs a Bing Search API key) both implement `ISearchTool` for the web-search fallback.
- **Planner.Tests** — unit tests for the plan generation/execution/evaluation loop.

## Running locally

You need Ollama (embeddings) and Qdrant (vector DB) running, with the `manuals` collection already populated by RAG_Chatbot's Ingestion project:

```
ollama pull all-minilm
ollama serve

docker run -p 6333:6333 -p 6334:6334 -v qdrant_storage:/qdrant/storage:z -d qdrant/qdrant
```

Set your AI endpoint and key via user secrets (from within `src/CorrectiveRetrievalAugmentedGenerationApp`):

```
dotnet user-secrets set "AI:Endpoint" "https://SOMETHING.openai.azure.com/"
dotnet user-secrets set "AI:Key" "YourKeyHere"
```

Then:

```
dotnet run --project src/CorrectiveRetrievalAugmentedGenerationApp
```
