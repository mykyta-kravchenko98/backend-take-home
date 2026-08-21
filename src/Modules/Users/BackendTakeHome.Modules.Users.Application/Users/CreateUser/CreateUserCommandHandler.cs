using BackendTakeHome.Common.Application.Messaging;
using BackendTakeHome.Common.Domain;
using BackendTakeHome.Modules.Users.Domain.Users;

namespace BackendTakeHome.Modules.Users.Application.Users.CreateUser;

public sealed class CreateUserCommandHandler(IUserRepository userRepository)
    : ICommandHandler<CreateUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        CreateUserCommand command,
        CancellationToken cancellationToken)
    {
        Result<User> result = User.Create(command.Username);

        if (result.IsFailure)
        {
            return Result<Guid>.Failure(result.Error);
        }

        await userRepository.AddAsync(result.Value, cancellationToken);

        return Result<Guid>.Success(result.Value.Id);
    }
}
