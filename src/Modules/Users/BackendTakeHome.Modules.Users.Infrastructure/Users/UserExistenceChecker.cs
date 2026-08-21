using BackendTakeHome.Modules.Users.Contracts;
using BackendTakeHome.Modules.Users.Domain.Users;

namespace BackendTakeHome.Modules.Users.Infrastructure.Users;

internal sealed class UserExistenceChecker(IUserRepository userRepository) : IUserExistenceChecker
{
    public Task<bool> ExistsAsync(Guid userId, CancellationToken cancellationToken = default) =>
        userRepository.ExistsAsync(userId, cancellationToken);
}
