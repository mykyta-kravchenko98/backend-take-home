namespace BackendTakeHome.Modules.Users.Domain.Users;

public interface IUserRepository
{
    Task AddAsync(User user, CancellationToken cancellationToken = default);

    Task<User?> GetByIdAsync(Guid userId, CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(Guid userId, CancellationToken cancellationToken = default);
}
