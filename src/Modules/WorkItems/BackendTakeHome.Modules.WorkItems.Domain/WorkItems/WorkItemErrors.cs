using BackendTakeHome.Common.Domain;

namespace BackendTakeHome.Modules.WorkItems.Domain.WorkItems;

public static class WorkItemErrors
{
    public static readonly Error InvalidName = new(
        "WorkItems.InvalidName",
        "Work item name must not be empty.",
        ErrorType.Validation);

    public static readonly Error InvalidAssigneeId = new(
        "WorkItems.InvalidAssigneeId",
        "Assignee ID must not be empty.",
        ErrorType.Validation);

    public static Error AssigneeNotFound(Guid assigneeId) => new(
        "WorkItems.AssigneeNotFound",
        $"The assignee with ID '{assigneeId}' was not found.",
        ErrorType.NotFound);
}
