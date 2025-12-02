using Microsoft.EntityFrameworkCore;
using POS.Domain.Products;
using POS.Infrastructure.Database;

namespace POS.Infrastructure.Repositories;
internal sealed class ProductRepository(ApplicationDbContext context) : IProductRepository
{
    public async Task<Product?> GetByIdAsync(
        Guid productId, 
        CancellationToken cancellationToken = default)
    {
        return await context.Products.FirstOrDefaultAsync(p => p.Id == productId, cancellationToken);
    }

    public void Insert(Product product)
    {
        context.Products.Add(product);
    }
}
