using BackendTakeHome.Common.Application.Messaging;

namespace BackendTakeHome.Modules.Users.Application.Users.CreateUser;

public sealed record CreateUserCommand(string Username) : ICommand<Guid>;
