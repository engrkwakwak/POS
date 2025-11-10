using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Domain.Brands;

namespace POS.Infrastructure.Database.Configurations;
internal sealed class BrandConfiguration : IEntityTypeConfiguration<Brand>
{
    public void Configure(EntityTypeBuilder<Brand> builder)
    {
        builder.ToTable("brands");

        builder.HasKey(b => b.Id);

        builder.ComplexProperty(
            b => b.Name,
            bld => bld.Property(e => e.Value)
                .HasColumnName("name")
                .HasMaxLength(100)
                .IsRequired());
    }
}
