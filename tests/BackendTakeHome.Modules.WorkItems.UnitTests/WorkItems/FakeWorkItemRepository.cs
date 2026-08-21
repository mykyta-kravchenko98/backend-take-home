using BackendTakeHome.Modules.WorkItems.Domain.WorkItems;

namespace BackendTakeHome.Modules.WorkItems.UnitTests.WorkItems;

internal sealed class FakeWorkItemRepository : IWorkItemRepository
{
    private readonly Dictionary<Guid, WorkItem> _workItems = [];

    public IReadOnlyCollection<WorkItem> WorkItems => _workItems.Values;

    public Task AddAsync(WorkItem workItem, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _workItems.Add(workItem.Id, workItem);
        return Task.CompletedTask;
    }

    public Task<IReadOnlyCollection<WorkItem>> GetByAssigneeIdAsync(
        Guid assigneeId,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        IReadOnlyCollection<WorkItem> workItems = _workItems.Values
            .Where(workItem => workItem.AssigneeId == assigneeId)
            .OrderBy(workItem => workItem.Id)
            .ToArray();

        return Task.FromResult(workItems);
    }
}
