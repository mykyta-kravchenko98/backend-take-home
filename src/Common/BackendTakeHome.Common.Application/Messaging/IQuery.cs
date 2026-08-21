using BackendTakeHome.Common.Domain;
using MediatR;

namespace BackendTakeHome.Common.Application.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
