using System.Collections.Concurrent;
using BackendTakeHome.Modules.Users.Domain.Users;

namespace BackendTakeHome.Modules.Users.Infrastructure.Users;

internal sealed class InMemoryUserRepository : IUserRepository
{
    private readonly ConcurrentDictionary<Guid, User> _users = new();

    public Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (!_users.TryAdd(user.Id, user))
        {
            throw new InvalidOperationException($"A user with ID '{user.Id}' already exists.");
        }

        return Task.CompletedTask;
    }

    public Task<User?> GetByIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _users.TryGetValue(userId, out User? user);
        return Task.FromResult(user);
    }

    public Task<bool> ExistsAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(_users.ContainsKey(userId));
    }
}
