using BackendTakeHome.Common.Application.Messaging;

namespace BackendTakeHome.Modules.Users.Application.Users.GetUserById;

public sealed record GetUserByIdQuery(Guid UserId) : IQuery<UserResponse>;
