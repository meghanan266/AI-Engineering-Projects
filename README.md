# AI Engineering Projects

AI/GenAI engineering portfolio (.NET, C#): RAG, agentic AI workflows, LLM integration, vector search, and prompt engineering — built with Azure OpenAI, Microsoft.Extensions.AI, and Qdrant. Practical, working applications, not notebooks or demos.

## Projects

- **[QuizApp](./QuizApp)** — A Blazor Server app that generates quiz questions with an LLM and grades free-text answers, with prompt-injection-resistant answer checking.
- **[Embeddings](./Embeddings)** — Text embedding demos: semantic similarity between words, cosine-similarity search over document titles, and FAISS-backed vector search over ~10,000 real GitHub issue titles.
- **[LLM_ChatBot_Part_1](./LLM_ChatBot_Part_1)** — Core LLM chat patterns: streaming/non-streaming completion, structured-output extraction from text, and live categorization of Hacker News stories.
- **[LLM_ChatBot_Part_2](./LLM_ChatBot_Part_2)** — Tool-calling chatbot plus a custom `IChatClient` middleware pipeline (forced-language replies, rate limiting, prompt-based function calling).
- **[RAG_Chatbot](./RAG_Chatbot)** — A citation-backed RAG chatbot over PDF product manuals, using Qdrant for vector search, with a separate LLM-judged evaluation pipeline.
- **[Traffic_Camera_Vision_Monitor](./Traffic_Camera_Vision_Monitor)** — A vision-LLM app that analyzes traffic camera images, extracts structured traffic data via tool calling, raises alerts for anomalies, and caches results with Redis.
- **[Realtime_Voice_Booking_Assistant](./Realtime_Voice_Booking_Assistant)** — A speech-to-speech voice assistant (Blazor + OpenAI Realtime API) that takes restaurant bookings by voice, with live transcription and tool calling.
- **[Agentic_RAG_Chatbot](./Agentic_RAG_Chatbot)** — Corrective RAG with an agentic fallback: grades retrieved context relevance, then runs a plan-generate/execute/evaluate loop with live web search when retrieval falls short.

More projects will be added here over time, each in its own top-level folder.
