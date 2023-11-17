using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace OnlineStore.App.Helpers;

public static class HealthChecks
{
    public static class HealthCheckEndpointConstants
    {
        public const string Health = nameof(Health);
    }

    public static IServiceCollection AddHealthCheckSupport(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddHealthChecks()
            .AddCheck(HealthCheckEndpointConstants.Health, () => HealthCheckResult.Healthy());

        return serviceCollection;
    }

    public static IApplicationBuilder MapHealthCheckSupport(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.MapHealthCheckEndpoint(HealthCheckEndpointConstants.Health);

        return applicationBuilder;
    }

    private static IApplicationBuilder MapHealthCheckEndpoint(this IApplicationBuilder applicationBuilder, string healthCheckName)
    {
        return applicationBuilder.UseHealthChecks(
            $"/{healthCheckName}",
            new HealthCheckOptions
            {
                Predicate = _ => _.Name == healthCheckName,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
    }
}
