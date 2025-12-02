using MediatR;
using POS.Api.Extensions;
using POS.Api.Infrastructure;
using POS.Application.Products.CreateVariant;
using POS.SharedKernel;

namespace POS.Api.Endpoints.Products;

internal sealed class CreateVariant : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("products/{productId:guid}/variants", async (
            Guid productId,
            CreateVariantRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateVariantCommand(
                productId,
                request.Barcode,
                request.PriceAmount,
                request.PriceCurrencyCode,
                request.PackageType,
                request.PackageQuantity,
                request.MeasurementAmount,
                request.UnitOfMeasureCode,
                request.IsVatable);

            Result<Guid> result = await sender.Send(command, cancellationToken);

            return result.Match(
                () => Results.CreatedAtRoute(nameof(GetVariantById), new { id = result.Value }),
                CustomResults.Problem);
        })
        .WithTags(Tags.Products);
    }
}
