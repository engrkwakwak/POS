namespace POS.Infrastructure.Outbox;

public interface IProcessOutboxMessagesJob
{
    Task ProcessAsync();
}
