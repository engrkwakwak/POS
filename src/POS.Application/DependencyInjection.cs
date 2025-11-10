using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using POS.Application.Abstractions.Behaviors;

namespace POS.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

            config.AddOpenBehavior(typeof(ExceptionHandlingPipelineBehavior<,>));
            config.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
            config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
            config.AddOpenBehavior(typeof(TransactionalPipelineBehavior<,>));
            config.AddOpenBehavior(typeof(QueryCachingPipelineBehavior<,>));
        });

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        return services;
    }
}
