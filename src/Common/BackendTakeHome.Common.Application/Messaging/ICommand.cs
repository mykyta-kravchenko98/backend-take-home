using BackendTakeHome.Common.Domain;
using MediatR;

namespace BackendTakeHome.Common.Application.Messaging;

public interface ICommand : IRequest<Result>;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>;
