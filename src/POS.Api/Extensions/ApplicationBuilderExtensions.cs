using Asp.Versioning.ApiExplorer;

namespace POS.Api.Extensions;

internal static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseSwaggerWithUi(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            IReadOnlyList<ApiVersionDescription> descriptions = app.DescribeApiVersions();

            foreach (ApiVersionDescription description in descriptions)
            {
                string url = $"/swagger/{description.GroupName}/swagger.json";
                string name = description.GroupName.ToUpperInvariant();

                options.SwaggerEndpoint(url, name);
            }
        });

        return app;
    }
}
