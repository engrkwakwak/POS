
using MediatR;
using POS.Api.Extensions;
using POS.Api.Infrastructure;
using POS.Application.Products;
using POS.Application.Products.GetById;
using POS.SharedKernel;

namespace POS.Api.Endpoints.Products;

internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("products/{id:guid}", async (
            Guid id,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var query = new GetProductByIdQuery(id);

            Result<ProductResponse> result = await sender.Send(query, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Products);
    }
}
