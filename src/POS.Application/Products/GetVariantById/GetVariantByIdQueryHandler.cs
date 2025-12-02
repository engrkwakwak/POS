using System.Data;
using Dapper;
using POS.Application.Abstractions.Data;
using POS.Application.Abstractions.Messaging;
using POS.Domain.Products;
using POS.SharedKernel;

namespace POS.Application.Products.GetVariantById;

internal sealed class GetVariantByIdQueryHandler(IDbConnectionFactory factory)
    : IQueryHandler<GetVariantByIdQuery, ProductVariantResponse>
{
    public async Task<Result<ProductVariantResponse>> Handle(GetVariantByIdQuery query, CancellationToken cancellationToken)
    {
        const string sql =
            """
            SELECT
            	id AS Id,
            	product_id AS ProductId,
            	sku ,
            	barcode,
            	is_vatable AS IsVatable,
            	price_amount AS PriceAmount,
            	price_currency AS PriceCurrency,
            	package_size_unit AS PackageSizeUnit,
            	package_size_value AS PackageSizeValue,
            	packaging_type AS PackagingType,
            	packaging_quantity AS PackagingQuantity
            FROM
            	public.product_variants
            WHERE id = @ProductVariantId
                AND product_id = @ProductId
            """;

        using IDbConnection connection = factory.GetOpenConnection();

        ProductVariantResponse? variant = await connection.QueryFirstOrDefaultAsync<ProductVariantResponse>(
            sql,
            query);

        if (variant is null)
        {
            return Result.Failure<ProductVariantResponse>(ProductErrors.NotFound(query.ProductVariantId));
        }

        return variant;
    }
}
