using Microsoft.Extensions.AI;

namespace Evaluation;

// Simpler evaluation approach: score the answer against the truth with a single label,
// rather than the separate relevance/groundedness/correctness scores in ThreeScoreEvaluation.
static class SingleScoreEvaluation
{
    public class Counters
    {
        public int Count;
        public double Total;
    }

    public static async Task RunAsync(
        IChatClient evaluationChatClient, EvalQuestion evalQuestion, string answerText,
        object outputLock, Counters counters)
    {
        var response = await evaluationChatClient.GetResponseAsync<ScoreResponse>($$"""
            There is an AI assistant that helps customer support staff to answer questions about products.
            You are evaluating the quality of the answer given by the AI assistant for the following question.

            <question>{{evalQuestion.Question}}</question>
            <truth>{{evalQuestion.Answer}}</truth>
            <answer_given>{{answerText}}</answer_given>

            Score how well answer_given represents the truth. You must first give a short justification for your score.
            The score must be one of the following labels
             * Awful: The answer contains no relevant information, or information that contradicts the truth
             * Poor: The answer fails to include key information from the truth
             * Good: The answer includes the main points from the truth, but misses some facts
             * Perfect: The answer gives all relevant facts from the truth, without anything that contradicts it

            Respond as JSON object of the form {
              "Justification": string, // Up to 10 words
              "ScoreLabel": string // One of "Awful", "Poor", "Good", "Perfect"
            }
            """);

        if (response.TryGetResult(out var score))
        {
            lock (outputLock)
            {
                counters.Count++;
                counters.Total += score.ScoreNumber;

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine($"Question {evalQuestion.QuestionId} scored {score.ScoreNumber} ({score.Justification})");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Average: {(counters.Total / counters.Count):F2} after {counters.Count} questions");
            }
        }
    }
}
