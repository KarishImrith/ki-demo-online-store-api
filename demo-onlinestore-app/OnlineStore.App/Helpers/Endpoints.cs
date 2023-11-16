using Ki.HealthChecks.Extensions;

namespace OnlineStore.App.Helpers;

public static class Endpoints
{
    public static class HealthCheckEndpointConstants
    {
        public const string Health = nameof(Health);
    }

    public static void UseEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        // Health Check Endpoints
        endpointRouteBuilder.MapHealthCheckEndpoint(HealthCheckEndpointConstants.Health);
    }
}
