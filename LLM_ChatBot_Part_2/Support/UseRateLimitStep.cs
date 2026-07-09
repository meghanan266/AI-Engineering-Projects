using System.Threading.RateLimiting;

namespace Microsoft.Extensions.AI;

public static class UseRateLimitStep
{
    public static ChatClientBuilder UseRateLimit(this ChatClientBuilder builder, TimeSpan window)
        => builder.Use(inner => new RateLimitedChatClient(inner, window));

    private sealed class RateLimitedChatClient(IChatClient inner, TimeSpan window) : DelegatingChatClient(inner)
    {
        // Note that this rate limit is enforced globally across all users on your site.
        // It's not a separate rate limit for each user. You could do that but the implementation would be a bit different.
        private readonly RateLimiter rateLimit = new FixedWindowRateLimiter(new() { Window = window, QueueLimit = 1, PermitLimit = 1 });

        public override async Task<ChatResponse> GetResponseAsync(IEnumerable<ChatMessage> messages, ChatOptions? options = null, CancellationToken cancellationToken = default)
        {
            using var lease = await rateLimit.AcquireAsync(cancellationToken: cancellationToken);
            return await base.GetResponseAsync(messages, options, cancellationToken);
        }
    }
}
