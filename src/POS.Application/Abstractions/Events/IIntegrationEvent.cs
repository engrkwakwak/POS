using MediatR;

namespace POS.Application.Abstractions.Events;

public interface IIntegrationEvent : INotification
{
    Guid Id { get; init; }
}
