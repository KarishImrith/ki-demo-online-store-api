using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OnlineStore.Database.Entities;

public class PurchaseOrder
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public User User { get; set; }

    public DateTimeOffset OrderDateTime { get; set; }

    public decimal TotalSellingPrice { get; set; }

    // Navigation
    public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }
}

public class PurchaseOrderEntityConfig : IEntityTypeConfiguration<PurchaseOrder>
{
    public void Configure(EntityTypeBuilder<PurchaseOrder> builder)
    {
        builder
            .HasOne(_ => _.User)
            .WithMany(_ => _.PurchaseOrders)
            .HasForeignKey(_ => _.UserId);

        builder
            .Property(_ => _.TotalSellingPrice)
            .HasColumnType("money");
    }
}
