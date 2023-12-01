using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OnlineStore.Database.Entities;

public class User : IdentityUser<long>
{
    // Navigation
    public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }

    public ICollection<PurchaseOrder> PurchaseOrders { get; set; }
}

public class UserEntityConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User));
    }
}
