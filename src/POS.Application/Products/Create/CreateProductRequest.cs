using POS.Domain.Products;

namespace POS.Application.Products.Create;
public sealed record CreateProductRequest(
    string Name,
    string Description,
    ProductCategory ProductCategory,
    Guid BrandId,
    bool IsVatable);
