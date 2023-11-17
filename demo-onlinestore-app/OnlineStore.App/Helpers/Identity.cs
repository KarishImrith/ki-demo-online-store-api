using OnlineStore.Database;
using OnlineStore.Database.Entities;

namespace OnlineStore.App.Helpers;

public static class Identity
{
    public static IServiceCollection AddIdentitySupport(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddIdentityApiEndpoints<User>()
            .AddEntityFrameworkStores<DataDbContext>();

        return serviceCollection;
    }
}
