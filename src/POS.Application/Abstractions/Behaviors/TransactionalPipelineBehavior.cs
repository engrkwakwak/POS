using MediatR;
using Microsoft.Extensions.Logging;
using POS.Application.Abstractions.Data;
using POS.Application.Abstractions.Messaging;
using POS.SharedKernel;

namespace POS.Application.Abstractions.Behaviors;

internal sealed class TransactionalPipelineBehavior<TRequest, TResponse>(
    IUnitOfWork unitOfWork,
    ILogger<TransactionalPipelineBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ITransactionalCommand
    where TResponse : Result
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Beginning transaction for {RequestName}", typeof(TRequest).Name);

        TResponse response = await next(cancellationToken);

        if (response.IsFailure)
        {
            logger.LogWarning("Skipping transaction commit for {RequestName} due to failure", typeof(TRequest).Name);
            return response;
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation("Committed transaction for {RequestName}", typeof(TRequest).Name);

        return response;
    }
}
