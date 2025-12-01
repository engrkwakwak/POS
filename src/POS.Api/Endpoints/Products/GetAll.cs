using MediatR;
using POS.Api.Extensions;
using POS.Api.Infrastructure;
using POS.Application.Products;
using POS.Application.Products.GetAll;
using POS.SharedKernel;

namespace POS.Api.Endpoints.Products;

internal sealed class GetAll : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("products", async (
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var query = new GetAllProductsQuery();

            Result<IReadOnlyList<ProductResponse>> result = await sender.Send(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Products);
    }
}
