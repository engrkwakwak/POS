using System.Data;
using Dapper;
using POS.Application.Abstractions.Data;
using POS.Application.Abstractions.Messaging;
using POS.Domain.Products;
using POS.SharedKernel;

namespace POS.Application.Products.GetById;

internal sealed class GetProductByIdQueryHandler(IDbConnectionFactory factory)
    : IQueryHandler<GetProductByIdQuery, ProductResponse>
{
    public async Task<Result<ProductResponse>> Handle(
        GetProductByIdQuery query, 
        CancellationToken cancellationToken)
    {
        const string sql =
            """
            SELECT 
            	p.id AS Id,
            	p.product_code AS ProductCode,
            	p.category AS Category,
            	p.brand_id AS BrandId,
            	p.is_vatable AS IsVatable,
            	p.description AS Description,
            	p."name" AS Name
            FROM
            	public.products p
            WHERE p.id = @ProductId
            """;

        using IDbConnection connection = factory.GetOpenConnection();

        ProductResponse? product = await connection.QueryFirstOrDefaultAsync<ProductResponse>(
            sql,
            query);

        if (product is null)
        {
            return Result.Failure<ProductResponse>(ProductErrors.NotFound(query.ProductId));
        }

        return product;
    }
}
