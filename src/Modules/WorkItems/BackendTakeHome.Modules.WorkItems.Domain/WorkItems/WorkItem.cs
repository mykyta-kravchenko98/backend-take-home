using BackendTakeHome.Common.Domain;

namespace BackendTakeHome.Modules.WorkItems.Domain.WorkItems;

public sealed class WorkItem
{
    private WorkItem(string name, Guid assigneeId)
    {
        Id = Guid.NewGuid();
        Name = name;
        AssigneeId = assigneeId;
    }

    public Guid Id { get; }

    public string Name { get; }

    public Guid AssigneeId { get; }

    public static Result<WorkItem> Create(string? name, Guid assigneeId)
    {
        string normalizedName = name?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(normalizedName))
        {
            return Result<WorkItem>.Failure(WorkItemErrors.InvalidName);
        }

        if (assigneeId == Guid.Empty)
        {
            return Result<WorkItem>.Failure(WorkItemErrors.InvalidAssigneeId);
        }

        return Result<WorkItem>.Success(new WorkItem(normalizedName, assigneeId));
    }
}
