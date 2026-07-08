# Embeddings

Two demos of using text embeddings for semantic similarity and search, built on `Microsoft.Extensions.AI`.

## How it works

- **SentenceSimilarity** — embeds a few words and compares them with cosine similarity to show how semantically similar words end up close together in vector space.
- **ManualSemanticSearch** — embeds a set of document titles, then lets you type free-text queries and returns the closest matching titles by cosine similarity.

Both use a local embedding model (`all-minilm`) served by Ollama.

## Running locally

Pull the embedding model and start Ollama:

```
ollama pull all-minilm
ollama serve
```

Then choose which demo to run by commenting/uncommenting the corresponding line in `Program.cs`, and run:

```
dotnet run
```
