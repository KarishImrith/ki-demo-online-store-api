using Microsoft.EntityFrameworkCore;
using OnlineStore.Database;

namespace OnlineStore.App.Helpers;

public static class Database
{
    public static IServiceCollection AddDatabaseSupport(this IServiceCollection serviceCollection)
    {
        // TODO: Change this to use SQL Server
        serviceCollection.AddDbContext<DataDbContext>(options => options.UseInMemoryDatabase(nameof(OnlineStore)));

        return serviceCollection;
    }
}
