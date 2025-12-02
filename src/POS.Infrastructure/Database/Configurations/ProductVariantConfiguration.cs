using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Domain.Products;
using POS.Domain.Shared;

namespace POS.Infrastructure.Database.Configurations;
internal sealed class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariant>
{
    public void Configure(EntityTypeBuilder<ProductVariant> builder)
    {
        builder.ToTable("product_variants");

        builder.HasKey(v => v.Id);

        builder.OwnsOne(
            v => v.Sku,
            skuBuilder =>
            {
                skuBuilder.Property(sku => sku.Value)
                    .HasColumnName("sku")
                    .HasMaxLength(12)
                    .IsRequired();
                skuBuilder.HasIndex(sku => sku.Value).IsUnique();
            });

        builder.OwnsOne(
            v => v.Barcode,
            barcodeBuilder =>
            {
                barcodeBuilder.Property(barcode => barcode.Value)
                    .HasColumnName("barcode")
                    .HasMaxLength(20)
                    .IsRequired();
                barcodeBuilder.HasIndex(barcode => barcode.Value).IsUnique();
            });

        builder.ComplexProperty(
            v => v.Price,
            priceBuilder =>
            {
                priceBuilder.Property(money => money.Amount)
                    .HasColumnName("price_amount")
                    .HasPrecision(18, 2)
                    .IsRequired();

                priceBuilder.Property(money => money.Currency)
                    .HasColumnName("price_currency")
                    .HasMaxLength(3)
                    .HasConversion(
                        currency => currency.Code,
                        code => Currency.FromCode(code))
                    .IsRequired();
            });

        builder.ComplexProperty(
            v => v.Packaging,
            packagingBuilder =>
            {
                packagingBuilder.Property(p => p.Type)
                    .HasColumnName("packaging_type")
                    .IsRequired()
                    .HasConversion<string>();

                packagingBuilder.Property(p => p.Quantity)
                     .HasColumnName("packaging_quantity")
                     .IsRequired();
            });

        builder.ComplexProperty(
            v => v.PackageSize,
            sizeBuilder =>
            {
                sizeBuilder.Property(s => s.Value)
                    .HasColumnName("package_size_value")
                    .HasPrecision(18, 2)
                    .IsRequired();

                sizeBuilder.Property(s => s.Unit)
                    .HasColumnName("package_size_unit")
                    .HasMaxLength(10)
                    .HasConversion(
                        unit => unit.Code,
                        code => UnitOfMeasure.FromCode(code)!);
            });

        builder.Property(v => v.IsVatable)
            .HasColumnName("is_vatable")
            .HasDefaultValue(true);
    }
}
