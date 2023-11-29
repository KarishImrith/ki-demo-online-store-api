using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OnlineStore.Database.Entities;

public class ShoppingCartItem
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public User User { get; set; }

    public long ProductId { get; set; }

    public Product Product { get; set; }

    public int Quantity { get; set; }

    public decimal SellingPrice { get; set; }

    public DateTimeOffset AddedDateTime { get; set; }

    public long? PurchaseOrderId { get; set; }

    public PurchaseOrder PurchaseOrder { get; set; }
}

public class ShoppingCartItemEntityConfig : IEntityTypeConfiguration<ShoppingCartItem>
{
    public void Configure(EntityTypeBuilder<ShoppingCartItem> builder)
    {
        builder
            .HasOne(_ => _.User)
            .WithMany(_ => _.ShoppingCartItems)
            .HasForeignKey(_ => _.UserId);

        builder
            .HasOne(_ => _.Product)
            .WithMany(_ => _.ShoppingCartItems)
            .HasForeignKey(_ => _.ProductId);

        builder
            .Property(_ => _.SellingPrice)
            .HasColumnType("money");

        builder
            .HasOne(_ => _.PurchaseOrder)
            .WithMany(_ => _.ShoppingCartItems)
            .HasForeignKey(_ => _.PurchaseOrderId)
            .IsRequired(false);
    }
}
