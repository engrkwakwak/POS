using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using POS.Application.Abstractions.Events;

namespace POS.Infrastructure.Events;

internal sealed class IntegrationEventProcessorJob(
    InMemoryMessageQueue queue,
    IServiceScopeFactory serviceScopeFactory,
    ILogger<IntegrationEventProcessorJob> logger)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (IIntegrationEvent integrationEvent in queue.Reader.ReadAllAsync(stoppingToken))
        {
            try
            {
                using IServiceScope scope = serviceScopeFactory.CreateScope();

                IPublisher publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();

                await publisher.Publish(integrationEvent, stoppingToken);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Something went wrong! {IntegrationEventId}", integrationEvent.Id);
            }
        }
    }
}
