using MediatR;
using POS.SharedKernel;

namespace POS.Application.Abstractions.Messaging;
public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
