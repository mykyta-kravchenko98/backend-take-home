using BackendTakeHome.Common.Domain;

namespace BackendTakeHome.Modules.Users.Domain.Users;

public static class UserErrors
{
    public static readonly Error InvalidUsername = new(
        "Users.InvalidUsername",
        "Username must not be empty.",
        ErrorType.Validation);

    public static Error NotFound(Guid userId) => new(
        "Users.NotFound",
        $"The user with ID '{userId}' was not found.",
        ErrorType.NotFound);
}
