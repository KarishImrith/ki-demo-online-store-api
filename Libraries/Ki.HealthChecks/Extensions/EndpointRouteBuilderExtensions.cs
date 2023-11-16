using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Routing;

namespace Ki.HealthChecks.Extensions;

public static class EndpointRouteBuilderExtensions
{
    public static IEndpointConventionBuilder MapHealthCheckEndpoint(this IEndpointRouteBuilder endpointRouteBuilder, string healthCheckName)
    {
        return endpointRouteBuilder.MapHealthChecks(
            $"{healthCheckName}",
            new HealthCheckOptions
            {
                Predicate = _ => _.Name == healthCheckName,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
    }
}
