using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Domain.Brands;
using POS.Domain.Products;

namespace POS.Infrastructure.Database.Configurations;
internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");

        builder.HasKey(p => p.Id);

        builder.ComplexProperty(
            p => p.Name,
            b => b.Property(e => e.Value)
                .HasColumnName("name")
                .HasMaxLength(100)
                .IsRequired());

        builder.ComplexProperty(
            p => p.Description,
            b => b.Property(e => e.Value)
                .HasColumnName("description")
                .HasMaxLength(500));

        builder.OwnsOne(
            p => p.ProductCode,
            pcBuilder =>
            {
                pcBuilder.Property(pc => pc.Value)
                    .HasColumnName("product_code")
                    .IsRequired();

                pcBuilder.HasIndex(pc => pc.Value).IsUnique();
            });

        builder.HasMany(p => p.Variants)
            .WithOne()
            .HasForeignKey(v => v.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<Brand>()
            .WithMany()
            .HasForeignKey(p => p.BrandId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
