using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace OnlineStore.App.Helpers;

public static class RateLimiter
{
    public static IServiceCollection AddRateLimiterSupport(this IServiceCollection serviceCollection, int permitLimit, int windowSeconds, int queueLimit)
    {
        serviceCollection
            .AddRateLimiter(options =>
            {
                options.AddFixedWindowLimiter(
                    policyName: nameof(OnlineStore),
                    configureOptions =>
                    {
                        configureOptions.PermitLimit = permitLimit;
                        configureOptions.Window = TimeSpan.FromSeconds(windowSeconds);
                        configureOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                        configureOptions.QueueLimit = queueLimit;
                    });
            });

        return serviceCollection;
    }
}
