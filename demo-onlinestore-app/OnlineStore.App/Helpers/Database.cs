using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Database;
using OnlineStore.Database.Entities;

namespace OnlineStore.App.Helpers;

public static class Database
{
    public static IServiceCollection AddDatabaseSupport(this IServiceCollection serviceCollection, string connectionString)
    {
        serviceCollection.AddDbContext<DataDbContext>(options => options.UseSqlServer(connectionString));

        return serviceCollection;
    }

    public static async Task EnsureDatabaseInitializedAsync(this IServiceProvider serviceProvider, string primaryUserEmailAddress, string primaryUserPassword)
    {
        using var scope = serviceProvider.CreateScope();

        var webHostEnvironment = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
        if (webHostEnvironment.IsDevelopment())
        {
            using var dataDbContext = scope.ServiceProvider.GetRequiredService<DataDbContext>();

            // Ensure latest migrations are applied
            await dataDbContext.Database.MigrateAsync();

            // Seed data
            using var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var primaryUser = new User { UserName = primaryUserEmailAddress, Email = primaryUserEmailAddress };
            await userManager.CreateAsync(primaryUser, primaryUserPassword);

            var sampleProduct = new Product { Name = "Sample Product", CurrentSellingPrice = 249.99M };
            await dataDbContext.AddAsync(sampleProduct);

            var sampleShoppingCartItem = new ShoppingCartItem
            {
                UserId = primaryUser.Id,
                Product = sampleProduct,
                Quantity = 10
            };
            await dataDbContext.AddAsync(sampleShoppingCartItem);

            await dataDbContext.SaveChangesAsync();
        }
    }
}
