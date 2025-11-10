namespace POS.Api.Endpoints;

internal interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}
