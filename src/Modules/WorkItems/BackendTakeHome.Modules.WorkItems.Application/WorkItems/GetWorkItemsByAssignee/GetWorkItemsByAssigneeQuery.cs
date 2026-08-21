using BackendTakeHome.Common.Application.Messaging;

namespace BackendTakeHome.Modules.WorkItems.Application.WorkItems.GetWorkItemsByAssignee;

public sealed record GetWorkItemsByAssigneeQuery(Guid AssigneeId)
    : IQuery<IReadOnlyCollection<WorkItemResponse>>;
