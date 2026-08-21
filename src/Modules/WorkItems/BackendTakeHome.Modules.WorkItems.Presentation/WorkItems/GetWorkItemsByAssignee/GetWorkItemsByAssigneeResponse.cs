namespace BackendTakeHome.Modules.WorkItems.Presentation.WorkItems.GetWorkItemsByAssignee;

public sealed record GetWorkItemsByAssigneeResponse(Guid Id, string Name, Guid AssigneeId);
