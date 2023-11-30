using OnlineStore.App.Factories;

namespace OnlineStore.App.Helpers;

public static class Controllers
{
    public static IMvcBuilder AddControllersSupport(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddControllers()
            .AddJsonOptions(configure => JsonSerializerOptionsFactory.ConfigureJsonSerializerOptions());
    }
}
