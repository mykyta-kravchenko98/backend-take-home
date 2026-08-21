using BackendTakeHome.Modules.WorkItems.Application.WorkItems.GetWorkItemsByAssignee;
using BackendTakeHome.Modules.WorkItems.Domain.WorkItems;
using Xunit;

namespace BackendTakeHome.Modules.WorkItems.UnitTests.WorkItems;

public sealed class GetWorkItemsByAssigneeQueryHandlerTests
{
    [Fact]
    public async Task Handle_returns_only_work_items_for_the_requested_assignee()
    {
        var repository = new FakeWorkItemRepository();
        Guid requestedAssigneeId = Guid.NewGuid();
        WorkItem first = WorkItem.Create("First", requestedAssigneeId).Value;
        WorkItem second = WorkItem.Create("Second", requestedAssigneeId).Value;
        WorkItem other = WorkItem.Create("Other", Guid.NewGuid()).Value;
        await repository.AddAsync(first);
        await repository.AddAsync(second);
        await repository.AddAsync(other);
        var handler = new GetWorkItemsByAssigneeQueryHandler(repository);

        var result = await handler.Handle(
            new GetWorkItemsByAssigneeQuery(requestedAssigneeId),
            CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Equal(2, result.Value.Count);
        Assert.All(result.Value, item => Assert.Equal(requestedAssigneeId, item.AssigneeId));
        Assert.Equal(
            new[] { first.Id, second.Id }.OrderBy(id => id),
            result.Value.Select(item => item.Id));
    }

    [Fact]
    public async Task Handle_returns_an_empty_collection_when_no_work_items_match()
    {
        var handler = new GetWorkItemsByAssigneeQueryHandler(new FakeWorkItemRepository());

        var result = await handler.Handle(
            new GetWorkItemsByAssigneeQuery(Guid.NewGuid()),
            CancellationToken.None);

        Assert.True(result.IsSuccess);
        Assert.Empty(result.Value);
    }
}
