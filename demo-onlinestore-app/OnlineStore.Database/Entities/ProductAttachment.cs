using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OnlineStore.Database.Entities;

public class ProductAttachment
{
    public long Id { get; set; }

    public long ProductId { get; set; }

    public Product Product { get; set; }

    public string FilePath { get; set; }

    public string FileName { get; set; }
}

public class ProductAttachmentEntityConfig : IEntityTypeConfiguration<ProductAttachment>
{
    public void Configure(EntityTypeBuilder<ProductAttachment> builder)
    {
        builder
            .HasOne(_ => _.Product)
            .WithMany(_ => _.ProductAttachments)
            .HasForeignKey(_ => _.ProductId);
    }
}
