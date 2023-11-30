using OnlineStore.Logic.Services;

namespace OnlineStore.App.Helpers;

public static class AutoMapper
{
    public static IServiceCollection AddAutoMapperSupport(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddAutoMapper(typeof(IIdentityService).Assembly);

        return serviceCollection;
    }
}
