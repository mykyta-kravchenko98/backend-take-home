using BackendTakeHome.Common.Application.Messaging;

namespace BackendTakeHome.Modules.WorkItems.Application.WorkItems.CreateWorkItem;

public sealed record CreateWorkItemCommand(string Name, Guid AssigneeId) : ICommand<Guid>;
