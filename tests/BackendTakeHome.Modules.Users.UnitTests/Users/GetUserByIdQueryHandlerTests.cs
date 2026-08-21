using BackendTakeHome.Modules.Users.Application.Users.GetUserById;
using BackendTakeHome.Modules.Users.Domain.Users;
using Xunit;

namespace BackendTakeHome.Modules.Users.UnitTests.Users;

public sealed class GetUserByIdQueryHandlerTests
{
    [Fact]
    public async Task Handle_returns_a_transport_safe_response_for_an_existing_user()
    {
        var repository = new FakeUserRepository();
        User user = User.Create("alice").Value;
        await repository.AddAsync(user);
        var handler = new GetUserByIdQueryHandler(repository);

        var result = await handler.Handle(new GetUserByIdQuery(user.Id), CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(user.Id, result.Value.Id);
        Assert.Equal(user.Username, result.Value.Username);
    }

    [Fact]
    public async Task Handle_returns_not_found_for_a_missing_user()
    {
        var repository = new FakeUserRepository();
        var handler = new GetUserByIdQueryHandler(repository);
        Guid missingUserId = Guid.NewGuid();

        var result = await handler.Handle(new GetUserByIdQuery(missingUserId), CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal(UserErrors.NotFound(missingUserId), result.Error);
    }
}
