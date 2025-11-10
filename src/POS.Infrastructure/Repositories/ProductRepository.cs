using POS.Domain.Products;
using POS.Infrastructure.Database;

namespace POS.Infrastructure.Repositories;
internal sealed class ProductRepository(ApplicationDbContext context) : IProductRepository
{
    public void Insert(Product product)
    {
        context.Products.Add(product);
    }
}
