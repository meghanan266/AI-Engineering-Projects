using Microsoft.Extensions.AI;
using System.Text.Json;

namespace Evaluation;

// Active evaluation approach: score the answer's context relevance, groundedness, and correctness
// separately, instead of a single combined score (see SingleScoreEvaluation).
// Note that ideally, "relevance" should be based on *all* the context we supply to the LLM, not just the citation it selects.
static class ThreeScoreEvaluation
{
    public class Counters
    {
        public int Count;
        public double ContextRelevance;
        public double AnswerGroundedness;
        public double AnswerCorrectness;
    }

    private static readonly JsonSerializerOptions OutputJsonOptions = new() { WriteIndented = true };

    public static async Task RunAsync(
        IChatClient evaluationChatClient, EvalQuestion evalQuestion, string answerText, string? context,
        object outputLock, Counters counters)
    {
        var response = await evaluationChatClient.GetResponseAsync<EvaluationResponse>($$"""
            There is an AI assistant that helps customer support staff to answer questions about products.
            You are evaluating the quality of the answer given by the AI assistant for the following question.

            <question>{{evalQuestion.Question}}</question>
            <truth>{{evalQuestion.Answer}}</truth>
            <context>{{context}}</context>
            <answer_given>{{answerText}}</answer_given>

            You are to provide three scores:

            1. Score the relevance of <context> to <question>.
               Ignore <truth> when scoring this. Does <context> contain information that may answer <question>?
            2. Score the groundedness of <answer_given> in <context>
               Ignore <truth> when scoring this. Does <answer_given> take its main claim from <context> alone?
            2. Score the correctness of <answer_given> based on <truth>.
               Does <answer_given> contain the facts from <truth>?

            Each score comes with a short justification, and must be one of the following labels:
             * Awful: it's completely unrelated to the target or contradicts it
             * Poor: it misses essential information from the target
             * Good: it includes the main information from the target, but misses smaller details
             * Perfect: it includes all important information from the target and does not contradict it

            Respond as JSON object of the form {
              "ContextRelevance": { "Justification": string, "ScoreLabel": string },
              "AnswerGroundedness": { "Justification": string, "ScoreLabel": string },
              "AnswerCorrectness": { "Justification": string, "ScoreLabel": string },
            }
            """);

        if (response.TryGetResult(out var score) && score.Populated)
        {
            lock (outputLock)
            {
                counters.Count++;
                counters.ContextRelevance += score.ContextRelevance!.ScoreNumber;
                counters.AnswerGroundedness += score.AnswerGroundedness!.ScoreNumber;
                counters.AnswerCorrectness += score.AnswerCorrectness!.ScoreNumber;

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(JsonSerializer.Serialize(score, OutputJsonOptions));

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Average: Context relevance {(counters.ContextRelevance / counters.Count):F2}, Groundedness {(counters.AnswerGroundedness / counters.Count):F2}, Correctness {(counters.AnswerCorrectness / counters.Count):F2} after {counters.Count} questions");
            }
        }
    }
}

class EvaluationResponse
{
    public ScoreResponse? ContextRelevance { get; set; }
    public ScoreResponse? AnswerGroundedness { get; set; }
    public ScoreResponse? AnswerCorrectness { get; set; }

    public bool Populated => ContextRelevance is not null && AnswerGroundedness is not null && AnswerCorrectness is not null;
}
