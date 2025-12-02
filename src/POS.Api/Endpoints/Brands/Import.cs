using MediatR;
using POS.Api.Extensions;
using POS.Api.Infrastructure;
using POS.Application.Brands.Import;
using POS.Domain.Brands;
using POS.SharedKernel;

namespace POS.Api.Endpoints.Brands;

internal sealed class Import : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("brands/import", async (
            IFormFile file,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            if (file.Length == 0)
            {
                return CustomResults.Problem(Result.Failure(BrandErrors.Upload.EmptyFile));
            }

            await using Stream stream = file.OpenReadStream();

            var command = new ImportBrandsCommand(stream, file.FileName, file.ContentType);

            Result result = await sender.Send(command, cancellationToken);

            return result.Match(
                () => Results.Accepted(),
                CustomResults.Problem);
        })
        .WithTags(Tags.Brands)
        .DisableAntiforgery();
    }
}
