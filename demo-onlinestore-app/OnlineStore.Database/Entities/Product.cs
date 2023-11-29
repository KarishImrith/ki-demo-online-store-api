using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OnlineStore.Database.Entities;

public class Product
{
    public long Id { get; set; }

    public string Name { get; set; }

    public decimal CurrentSellingPrice { get; set; }

    // Navigation
    public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }

    public ICollection<ProductAttachment> ProductAttachments { get; set; }
}

public class ProductEntityConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder
            .Property(_ => _.CurrentSellingPrice)
            .HasColumnType("money");
    }
}
