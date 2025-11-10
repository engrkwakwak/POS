using POS.Application.Abstractions.Messaging;
using POS.Domain.Products;

namespace POS.Application.Products.Create;

public record CreateProductCommand(
    string Name,
    string Description,
    ProductCategory ProductCategory,
    Guid BrandId,
    bool IsVatable) : ICommand<Guid>;
