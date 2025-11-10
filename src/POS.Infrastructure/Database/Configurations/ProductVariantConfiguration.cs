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
                    .HasMaxLength(100)
                    .IsRequired();
                barcodeBuilder.HasIndex(barcode => barcode.Value).IsUnique();
            });

        builder.ComplexProperty(
            v => v.Price,
            priceBuilder =>
            {
                priceBuilder.Property(money => money.Amount)
                    .HasColumnName("price_amount");

                priceBuilder.Property(money => money.Currency)
                    .HasColumnName("price_currency")
                    .HasMaxLength(3)
                    .HasConversion(
                        currency => currency.Code,
                        code => Currency.FromCode(code));
            });

        builder.ComplexProperty(
            v => v.UnitOfMeasure,
            uomBuilder =>
            {
                uomBuilder.Property(uom => uom.Type)
                    .HasColumnName("unit_of_measure_type");

                uomBuilder.Property(uom => uom.ItemsPerCase)
                    .HasColumnName("unit_of_measure_items_per_case");
            });

        builder.ComplexProperty(
            v => v.Size,
            sizeBuilder =>
            {
                sizeBuilder.Property(size => size.Value)
                    .HasColumnName("package_size_value");

                sizeBuilder.Property(size => size.Unit)
                    .HasColumnName("package_size_unit");
            });
    }
}
