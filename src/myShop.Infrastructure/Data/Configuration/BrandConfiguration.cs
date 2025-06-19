using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace myShop.Infrastructure.Data.Configuration;

public class BrandConfiguration : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.ToTable("Brand");
        builder.Property(p => p.Name).HasMaxLength(100);
    }
}