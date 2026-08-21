namespace BackendTakeHome.Modules.WorkItems.Presentation.WorkItems.GetWorkItemsByAssignee;

public sealed record GetWorkItemsByAssigneeRequest
{
    public Guid AssigneeId { get; init; }
}
