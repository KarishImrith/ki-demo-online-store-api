using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OnlineStore.Database.Entities;

public class Role : IdentityRole<long>
{
}

public class UserRoleEntityConfig : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable(nameof(Role));
    }
}
