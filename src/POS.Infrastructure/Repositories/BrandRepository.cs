using Microsoft.EntityFrameworkCore;
using POS.Domain.Brands;
using POS.Infrastructure.Database;

namespace POS.Infrastructure.Repositories;
internal sealed class BrandRepository(ApplicationDbContext context) : IBrandRepository
{
    public Task<bool> ExistsAsync(Guid brandId, CancellationToken cancellationToken = default)
    {
        return context.Brands
            .AnyAsync(brand => brand.Id == brandId, cancellationToken);
    }
}
