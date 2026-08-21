namespace BackendTakeHome.Modules.WorkItems.Domain.WorkItems;

public interface IWorkItemRepository
{
    Task AddAsync(WorkItem workItem, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<WorkItem>> GetByAssigneeIdAsync(
        Guid assigneeId,
        CancellationToken cancellationToken = default);
}
