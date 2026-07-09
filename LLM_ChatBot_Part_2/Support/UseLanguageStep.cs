namespace Microsoft.Extensions.AI;

public static class UseLanguageStep
{
    // This is an extension method that lets you add UseLanguageChatClient into a pipeline
    public static ChatClientBuilder UseLanguage(this ChatClientBuilder builder, string language)
    {
        return builder.Use(inner => new UseLanguageChatClient(inner, language));
    }

    // This is the actual middleware implementation
    private sealed class UseLanguageChatClient(IChatClient next, string language) : DelegatingChatClient(next)
    {
        public override Task<ChatResponse> GetResponseAsync(IEnumerable<ChatMessage> messages, ChatOptions? options = null, CancellationToken cancellationToken = default)
        {
            // Add an extra prompt
            var promptAugmentation = new ChatMessage(ChatRole.User, $"Always reply in the language {language}");
            return base.GetResponseAsync([.. messages, promptAugmentation], options, cancellationToken);
        }
    }
}
