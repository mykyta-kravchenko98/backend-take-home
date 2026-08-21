using BackendTakeHome.Modules.Users.Application.Users.CreateUser;
using Xunit;

namespace BackendTakeHome.Modules.Users.UnitTests.Users;

public sealed class CreateUserCommandHandlerTests
{
    [Fact]
    public async Task Handle_creates_and_persists_a_user()
    {
        var repository = new FakeUserRepository();
        var handler = new CreateUserCommandHandler(repository);

        var result = await handler.Handle(new CreateUserCommand("  alice  "), CancellationToken.None);

        Assert.True(result.IsSuccess);
        var user = Assert.Single(repository.Users);
        Assert.Equal(result.Value, user.Id);
        Assert.Equal("alice", user.Username);
    }

    [Fact]
    public async Task Handle_does_not_persist_an_invalid_user()
    {
        var repository = new FakeUserRepository();
        var handler = new CreateUserCommandHandler(repository);

        var result = await handler.Handle(new CreateUserCommand(" "), CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Empty(repository.Users);
    }
}
