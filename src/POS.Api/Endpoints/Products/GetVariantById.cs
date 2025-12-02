using MediatR;
using POS.Api.Extensions;
using POS.Api.Infrastructure;
using POS.Application.Products;
using POS.Application.Products.GetVariantById;
using POS.SharedKernel;

namespace POS.Api.Endpoints.Products;

internal sealed class GetVariantById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("products/{productId:guid}/variants/{id:guid}", async (
            Guid productId,
            Guid id,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var query = new GetVariantByIdQuery(productId, id);

            Result<ProductVariantResponse> result = await sender.Send(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Products);
    }
}
