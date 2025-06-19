using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace myShop.Infrastructure.Data.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Product");
        builder.Property(p => p.Name).HasMaxLength(50);
        builder.HasOne(p => p.Brand).WithMany();
        builder.HasIndex(p => p.Name);
    }
}