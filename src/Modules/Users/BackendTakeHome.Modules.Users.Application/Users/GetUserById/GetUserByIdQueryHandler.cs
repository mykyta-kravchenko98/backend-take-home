using BackendTakeHome.Common.Application.Messaging;
using BackendTakeHome.Common.Domain;
using BackendTakeHome.Modules.Users.Domain.Users;

namespace BackendTakeHome.Modules.Users.Application.Users.GetUserById;

public sealed class GetUserByIdQueryHandler(IUserRepository userRepository)
    : IQueryHandler<GetUserByIdQuery, UserResponse>
{
    public async Task<Result<UserResponse>> Handle(
        GetUserByIdQuery query,
        CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetByIdAsync(query.UserId, cancellationToken);

        return user is null
            ? Result<UserResponse>.Failure(UserErrors.NotFound(query.UserId))
            : Result<UserResponse>.Success(new UserResponse(user.Id, user.Username));
    }
}
