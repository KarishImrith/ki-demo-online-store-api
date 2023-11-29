using Microsoft.EntityFrameworkCore;
using OnlineStore.Database;

namespace OnlineStore.App.Helpers;

public static class Database
{
    public static IServiceCollection AddDatabaseSupport(this IServiceCollection serviceCollection, string connectionString)
    {
        serviceCollection.AddDbContext<DataDbContext>(options => options.UseSqlServer(connectionString));

        return serviceCollection;
    }

    public static void EnsureDatabaseMigrationsApplied(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var webHostEnvironment = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
        if (webHostEnvironment.IsDevelopment())
        {
            var dataDbContext = scope.ServiceProvider.GetRequiredService<DataDbContext>();
            dataDbContext.Database.Migrate();
        }
    }
}
