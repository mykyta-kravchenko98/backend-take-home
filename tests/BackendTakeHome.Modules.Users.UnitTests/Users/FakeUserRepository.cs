using BackendTakeHome.Modules.Users.Domain.Users;

namespace BackendTakeHome.Modules.Users.UnitTests.Users;

internal sealed class FakeUserRepository : IUserRepository
{
    private readonly Dictionary<Guid, User> _users = [];

    public IReadOnlyCollection<User> Users => _users.Values;

    public Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _users.Add(user.Id, user);
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
