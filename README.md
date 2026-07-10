# AI Engineering Projects

AI/GenAI engineering portfolio (.NET, C#): RAG, agentic AI workflows, LLM integration, vector search, and prompt engineering — built with Azure OpenAI, Microsoft.Extensions.AI, and Qdrant. Eight practical, working applications, not notebooks or demos.

## Skills demonstrated

- **LLM integration** — chat completion (streaming and non-streaming), structured/typed output extraction, function/tool calling, prompt engineering, token usage tracking
- **Retrieval-augmented generation (RAG)** — PDF ingestion and chunking, embedding generation, vector search with Qdrant, citation-backed answers, LLM-judged evaluation pipelines
- **Agentic AI** — corrective RAG with context-relevance grading, plan-generate/execute/evaluate loops, fallback to live web search (Bing/DuckDuckGo) when retrieval is insufficient
- **Vector search & embeddings** — cosine-similarity search, FAISS-backed HNSW indexing at scale (~10k+ real-world documents), local vs. hosted embedding models
- **Custom `IChatClient` middleware** — building composable pipeline steps (rate limiting, forced-language output, distributed response caching, prompt-based function-calling shims for models without native tool support)
- **Multimodal AI** — vision-LLM image analysis with structured extraction and tool-triggered alerting
- **Realtime/voice AI** — speech-to-speech conversation via the OpenAI Realtime API, live transcription, audio streaming with interrupt handling
- **Full-stack .NET** — Blazor Server UIs, ASP.NET Core hosting, dependency injection, `dotnet user-secrets` for credential management, integration with Redis and Qdrant

## Projects

| Project | What it does |
|---|---|
| **[QuizApp](./QuizApp)** | Blazor Server app that generates quiz questions with an LLM and grades free-text answers, with prompt-injection-resistant answer checking. |
| **[Embeddings](./Embeddings)** | Semantic similarity between words, cosine-similarity search over document titles, and FAISS-backed HNSW vector search over ~10,000 real GitHub issue titles. |
| **[LLM_ChatBot_Part_1](./LLM_ChatBot_Part_1)** | Core LLM chat patterns: streaming/non-streaming completion, structured-output extraction from text, live categorization of Hacker News stories. |
| **[LLM_ChatBot_Part_2](./LLM_ChatBot_Part_2)** | Tool-calling chatbot plus a custom `IChatClient` middleware pipeline (forced-language replies, rate limiting, prompt-based function calling). |
| **[RAG_Chatbot](./RAG_Chatbot)** | Citation-backed RAG chatbot over PDF product manuals, using Qdrant for vector search, with a separate LLM-judged evaluation pipeline. |
| **[Agentic_RAG_Chatbot](./Agentic_RAG_Chatbot)** | Corrective RAG with an agentic fallback: grades retrieved context relevance, then runs a plan-generate/execute/evaluate loop with live web search when retrieval falls short. |
| **[Traffic_Camera_Vision_Monitor](./Traffic_Camera_Vision_Monitor)** | Vision-LLM app that analyzes traffic camera images, extracts structured traffic data via tool calling, raises alerts for anomalies, and caches results with Redis. |
| **[Realtime_Voice_Booking_Assistant](./Realtime_Voice_Booking_Assistant)** | Speech-to-speech voice assistant (Blazor + OpenAI Realtime API) that takes restaurant bookings by voice, with live transcription and tool calling. |

## Tech stack

**Languages/frameworks:** C#, .NET 9, Blazor Server, ASP.NET Core
**AI:** Microsoft.Extensions.AI, Azure OpenAI, OpenAI (Chat, Realtime, Whisper), Ollama
**Data/infra:** Qdrant (vector DB), FAISS, Redis, PdfPig (PDF parsing)

Each project folder is self-contained with its own README covering what it does and how to run it locally.
