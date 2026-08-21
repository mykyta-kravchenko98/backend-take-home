using BackendTakeHome.Modules.WorkItems.Domain.WorkItems;
using Xunit;

namespace BackendTakeHome.Modules.WorkItems.UnitTests.WorkItems;

public sealed class WorkItemTests
{
    [Fact]
    public void Create_generates_an_id_and_normalizes_the_name()
    {
        Guid assigneeId = Guid.NewGuid();

        var result = WorkItem.Create("  Prepare report  ", assigneeId);

        Assert.True(result.IsSuccess);
        Assert.NotEqual(Guid.Empty, result.Value.Id);
        Assert.Equal("Prepare report", result.Value.Name);
        Assert.Equal(assigneeId, result.Value.AssigneeId);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Create_rejects_an_empty_name(string? name)
    {
        var result = WorkItem.Create(name, Guid.NewGuid());

        Assert.True(result.IsFailure);
        Assert.Equal(WorkItemErrors.InvalidName, result.Error);
    }

    [Fact]
    public void Create_rejects_an_empty_assignee_id()
    {
        var result = WorkItem.Create("Prepare report", Guid.Empty);

        Assert.True(result.IsFailure);
        Assert.Equal(WorkItemErrors.InvalidAssigneeId, result.Error);
    }
}
