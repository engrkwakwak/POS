using POS.Application.Abstractions.Events;

namespace POS.Infrastructure.Events;

internal sealed class EventBus(InMemoryMessageQueue queue) : IEventBus
{
    public async Task PublishAsync<T>(T integrationEvent, CancellationToken cancellationToken = default)
        where T : class, IIntegrationEvent
    {
        await queue.Writer.WriteAsync(integrationEvent, cancellationToken);
    }
}
