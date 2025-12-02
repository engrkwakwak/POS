using MediatR;
using POS.Api.Extensions;
using POS.Api.Infrastructure;
using POS.Application.Products.Create;
using POS.SharedKernel;

namespace POS.Api.Endpoints.Products;

internal sealed class Create : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("products", async (
            CreateProductRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateProductCommand(
                request.Name,
                request.Description,
                request.ProductCategory,
                request.BrandId,
                request.IsVatable);

            Result<Guid> result = await sender.Send(command, cancellationToken);

            return result.Match(
                () => Results.CreatedAtRoute("Get", new { id = result.Value }),
                CustomResults.Problem);
        })
        .WithTags(Tags.Products);
    }
}
