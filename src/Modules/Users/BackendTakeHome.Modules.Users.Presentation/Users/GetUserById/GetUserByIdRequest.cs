namespace BackendTakeHome.Modules.Users.Presentation.Users.GetUserById;

public sealed record GetUserByIdRequest
{
    public Guid Id { get; init; }
}
