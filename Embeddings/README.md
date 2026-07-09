# Embeddings

Demos of using text embeddings for semantic similarity and search, built on `Microsoft.Extensions.AI`.

## How it works

- **SentenceSimilarity** — embeds a few words and compares them with cosine similarity to show how semantically similar words end up close together in vector space.
- **ManualSemanticSearch** — embeds a set of document titles, then lets you type free-text queries and returns the closest matching titles by cosine similarity.
- **FaissSemanticSearch** — builds a [FAISS](https://github.com/facebookresearch/faiss) HNSW vector index over ~10,000 real GitHub issue titles from `dotnet/runtime`, persists it to disk, then lets you query it interactively with nearest-neighbor search and reports search latency.

SentenceSimilarity and ManualSemanticSearch use a local embedding model (`all-minilm`) served by Ollama. FaissSemanticSearch embeds in-process on CPU using a small local embedding model, so it needs no external server.

## Running locally

For SentenceSimilarity / ManualSemanticSearch, pull the embedding model and start Ollama:

```
ollama pull all-minilm
ollama serve
```

Then choose which demo to run by commenting/uncommenting the corresponding line in `Program.cs`, and run:

```
dotnet run
```

FaissSemanticSearch needs the native FAISS library at runtime — this is bundled for Linux under `runtimes/`; on Windows it's pulled in automatically via the `FaissNet` NuGet package. The first run builds and saves the index to `index_hnsw.bin` (ignored by git); subsequent runs load it directly.
