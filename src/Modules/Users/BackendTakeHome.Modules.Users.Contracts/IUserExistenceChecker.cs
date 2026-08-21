namespace BackendTakeHome.Modules.Users.Contracts;

public interface IUserExistenceChecker
{
    Task<bool> ExistsAsync(Guid userId, CancellationToken cancellationToken = default);
}
