namespace BackendTakeHome.Modules.WorkItems.Application.WorkItems.GetWorkItemsByAssignee;

public sealed record WorkItemResponse(Guid Id, string Name, Guid AssigneeId);
