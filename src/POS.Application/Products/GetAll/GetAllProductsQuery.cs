using POS.Application.Abstractions.Messaging;

namespace POS.Application.Products.GetAll;

public sealed record GetAllProductsQuery 
    : IQuery<IReadOnlyList<ProductResponse>>;
