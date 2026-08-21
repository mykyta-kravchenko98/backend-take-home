using System.Collections.Concurrent;
using BackendTakeHome.Modules.WorkItems.Domain.WorkItems;

namespace BackendTakeHome.Modules.WorkItems.Infrastructure.WorkItems;

internal sealed class InMemoryWorkItemRepository : IWorkItemRepository
{
    private readonly ConcurrentDictionary<Guid, WorkItem> _workItems = new();

    public Task AddAsync(WorkItem workItem, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (!_workItems.TryAdd(workItem.Id, workItem))
        {
            throw new InvalidOperationException($"A work item with ID '{workItem.Id}' already exists.");
        }

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
