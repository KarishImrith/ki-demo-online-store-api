using OnlineStore.Logic.Services;

namespace OnlineStore.App.Helpers;

public static class Logic
{
    public static IServiceCollection AddLogicSupport(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IIdentityService, IdentityService>();

        return serviceCollection;
    }
}
