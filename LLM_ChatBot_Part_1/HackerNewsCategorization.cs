using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.AI;

namespace Chat;

internal static class HackerNewsCategorization
{
    public static async Task RunAsync(IChatClient chatClient)
    {
        var stories = await GetTopStories(20);

        // Categorize them all at once
        var response = await chatClient.GetResponseAsync<CategorizedHNStory[]>(
            $"For each of the following news stories, decide on a suitable category: {JsonSerializer.Serialize(stories)}");

        // Display results
        if (response.TryGetResult(out var categorized))
        {
            foreach (var group in categorized.GroupBy(s => s.Category))
            {
                Console.WriteLine(group.Key);
                foreach (var story in group)
                {
                    Console.WriteLine($" * [{story.Id}] {story.Title}");
                }
                Console.WriteLine();
            }
        }
    }

    private static async Task<HNStory[]> GetTopStories(int count)
    {
        const string baseUrl = "https://hacker-news.firebaseio.com/v0";
        using var client = new HttpClient();
        var storyIds = await client.GetFromJsonAsync<int[]>($"{baseUrl}/topstories.json");
        var resultTasks = storyIds!.Take(count).Select(id => client.GetFromJsonAsync<HNStory>($"{baseUrl}/item/{id}.json")).ToArray();
        return (await Task.WhenAll(resultTasks))!;
    }

    record HNStory(int Id, string Title);
    record CategorizedHNStory(int Id, string Title, Category Category);
    enum Category { AI, ProgrammingLanguages, Startups, History, Business, Society }
}
