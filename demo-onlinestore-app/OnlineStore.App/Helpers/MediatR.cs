using OnlineStore.Logic.Services;

namespace OnlineStore.App.Helpers;

public static class MediatR
{
    public static IServiceCollection AddMediatRSupport(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddMediatR(configuration => configuration.RegisterServicesFromAssemblies(typeof(IdentityService).Assembly));

        return serviceCollection;
    }
}
