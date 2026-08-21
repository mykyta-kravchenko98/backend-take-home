using BackendTakeHome.Modules.Users.Domain.Users;
using Xunit;

namespace BackendTakeHome.Modules.Users.UnitTests.Users;

public sealed class UserTests
{
    [Fact]
    public void Create_generates_an_id_and_normalizes_the_username()
    {
        var result = User.Create("  alice  ");

        Assert.True(result.IsSuccess);
        Assert.NotEqual(Guid.Empty, result.Value.Id);
        Assert.Equal("alice", result.Value.Username);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_rejects_an_empty_username(string? username)
    {
        var result = User.Create(username);

        Assert.True(result.IsFailure);
        Assert.Equal(UserErrors.InvalidUsername, result.Error);
    }
}
