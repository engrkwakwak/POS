namespace POS.SharedKernel;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
