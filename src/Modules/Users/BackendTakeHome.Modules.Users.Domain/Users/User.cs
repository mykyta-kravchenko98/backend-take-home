using BackendTakeHome.Common.Domain;

namespace BackendTakeHome.Modules.Users.Domain.Users;

public sealed class User
{
    private User(string username)
    {
        Id = Guid.NewGuid();
        Username = username;
    }

    public Guid Id { get; }

    public string Username { get; }

    public static Result<User> Create(string? username)
    {
        string normalizedUsername = username?.Trim() ?? string.Empty;

        return string.IsNullOrWhiteSpace(normalizedUsername)
            ? Result<User>.Failure(UserErrors.InvalidUsername)
            : Result<User>.Success(new User(normalizedUsername));
    }
}
