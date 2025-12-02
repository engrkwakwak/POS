using Microsoft.EntityFrameworkCore;
using POS.Domain.Products;
using POS.Infrastructure.Database;

namespace POS.Infrastructure.Repositories;

internal sealed class ProductVariantRepository(
    ApplicationDbContext context) : IProductVariantRepository
{
    public async Task<bool> IsBarcodeUniqueAsync(
        string barcode, 
        CancellationToken cancellationToken = default)
    {
        return !await context.ProductVariants
            .AnyAsync(pv => pv.Barcode.Value == barcode, cancellationToken);
    }
}
