using System.Data;

namespace POS.Application.Abstractions.Data;
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    Task<IDbTransaction> BeginTransactionAsync();
}
