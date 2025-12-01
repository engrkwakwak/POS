using Microsoft.EntityFrameworkCore;
using POS.Domain.Brands;
using POS.Infrastructure.Database;

namespace POS.Infrastructure.Repositories;
internal sealed class BrandRepository(ApplicationDbContext context) : IBrandRepository
{
    public async Task<bool> ExistsAsync(
        Guid brandId, 
        CancellationToken cancellationToken = default)
    {
        return await context.Brands
            .AnyAsync(brand => brand.Id == brandId, cancellationToken);
    }

    public async Task<List<Brand>> GetByNamesAsync(
        IEnumerable<string> names, 
        CancellationToken cancellationToken = default)
    {
        return await context.Brands
            .Where(brand => names.Contains(brand.Name.Value))
            .ToListAsync(cancellationToken);
    }

    public void Insert(Brand brand)
    {
        context.Brands.Add(brand);
    }
}
