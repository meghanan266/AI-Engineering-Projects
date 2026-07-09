# RAG Chatbot

A retrieval-augmented generation chatbot that answers questions about a product using its PDF manual, with citations back to the source page. Built on `Microsoft.Extensions.AI` and Qdrant.

## Projects

- **Ingestion** — parses each PDF in `data/product-manuals`, chunks the text into paragraphs, embeds each chunk, and stores it in a Qdrant vector collection called `manuals`, tagged with product ID and page number.
- **RetrievalAugmentedGenerationApp** — an interactive console chatbot. For a chosen product, it embeds the user's question, retrieves the most relevant manual chunks from Qdrant, and answers using the LLM with a citation (product/page/quote) back to the source.
- **Evaluation** — runs a batch of predefined questions (`data/evalquestions.json`) through the chatbot and scores each answer's quality using a separate LLM judge (relevancy/faithfulness/citation-accuracy style scoring).

## Running locally

You need Ollama (for embeddings) and Qdrant (vector DB) running:

```
ollama pull all-minilm
ollama serve

docker run -p 6333:6333 -p 6334:6334 -v qdrant_storage:/qdrant/storage:z -d qdrant/qdrant
```

Set your AI endpoint and key via user secrets (from within `src/RetrievalAugmentedGenerationApp` and `src/Evaluation`):

```
dotnet user-secrets set "AI:Endpoint" "https://SOMETHING.openai.azure.com/"
dotnet user-secrets set "AI:Key" "YourKeyHere"
```

Then, in order:

```
dotnet run --project src/Ingestion                          # one-time: index the manuals
dotnet run --project src/RetrievalAugmentedGenerationApp     # chat interactively
dotnet run --project src/Evaluation                          # or run the scored evaluation batch
```
