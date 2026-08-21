using BackendTakeHome.Modules.Users.Contracts;

namespace BackendTakeHome.Modules.WorkItems.UnitTests.WorkItems;

internal sealed class StubUserExistenceChecker(bool exists) : IUserExistenceChecker
{
    public Task<bool> ExistsAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(exists);
    }
}
