using BackendTakeHome.Common.Domain;
using BackendTakeHome.Modules.WorkItems.Application.WorkItems.CreateWorkItem;
using BackendTakeHome.Modules.WorkItems.Domain.WorkItems;
using Xunit;

namespace BackendTakeHome.Modules.WorkItems.UnitTests.WorkItems;

public sealed class CreateWorkItemCommandHandlerTests
{
    [Fact]
    public async Task Handle_creates_and_persists_a_work_item_for_an_existing_assignee()
    {
        var repository = new FakeWorkItemRepository();
        var handler = new CreateWorkItemCommandHandler(
            repository,
            new StubUserExistenceChecker(true));
        Guid assigneeId = Guid.NewGuid();

        var result = await handler.Handle(
            new CreateWorkItemCommand("  Prepare report  ", assigneeId),
            CancellationToken.None);

        Assert.True(result.IsSuccess);
        WorkItem workItem = Assert.Single(repository.WorkItems);
        Assert.Equal(result.Value, workItem.Id);
        Assert.Equal("Prepare report", workItem.Name);
        Assert.Equal(assigneeId, workItem.AssigneeId);
    }

    [Fact]
    public async Task Handle_returns_not_found_and_does_not_persist_for_a_missing_assignee()
    {
        var repository = new FakeWorkItemRepository();
        var handler = new CreateWorkItemCommandHandler(
            repository,
            new StubUserExistenceChecker(false));
        Guid assigneeId = Guid.NewGuid();

        var result = await handler.Handle(
            new CreateWorkItemCommand("Prepare report", assigneeId),
            CancellationToken.None);

        Assert.True(result.IsFailure);
        Assert.Equal(WorkItemErrors.AssigneeNotFound(assigneeId), result.Error);
        Assert.Equal(ErrorType.NotFound, result.Error.Type);
        Assert.Empty(repository.WorkItems);
    }
}
