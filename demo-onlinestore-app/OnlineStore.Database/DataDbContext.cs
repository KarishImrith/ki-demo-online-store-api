using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Database.Entities;

namespace OnlineStore.Database;

public class DataDbContext : IdentityDbContext<User>
{
    public DataDbContext(DbContextOptions<DataDbContext> options)
        : base(options)
    {
    }

    // Disable synchronous SaveChanges as we want to enforce asynchronous only usage
    public override int SaveChanges()
        => throw new NotImplementedException();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
