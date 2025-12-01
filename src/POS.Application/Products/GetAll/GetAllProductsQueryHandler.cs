using System.Data;
using POS.Application.Abstractions.Data;
using POS.Application.Abstractions.Messaging;
using POS.SharedKernel;
using Dapper;

namespace POS.Application.Products.GetAll;

internal sealed class GetAllProductsQueryHandler(IDbConnectionFactory factory)
    : IQueryHandler<GetAllProductsQuery, IReadOnlyList<ProductResponse>>
{
    public async Task<Result<IReadOnlyList<ProductResponse>>> Handle(
        GetAllProductsQuery request,
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
            ORDER BY
            	p."name" 
            """;

        using IDbConnection connection = factory.GetOpenConnection();

        IEnumerable<ProductResponse> products = await connection.QueryAsync<ProductResponse>(sql);

        return products.ToList();
    }
}
