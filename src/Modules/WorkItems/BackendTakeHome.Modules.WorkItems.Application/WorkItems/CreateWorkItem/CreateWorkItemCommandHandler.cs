using BackendTakeHome.Common.Application.Messaging;
using BackendTakeHome.Common.Domain;
using BackendTakeHome.Modules.Users.Contracts;
using BackendTakeHome.Modules.WorkItems.Domain.WorkItems;

namespace BackendTakeHome.Modules.WorkItems.Application.WorkItems.CreateWorkItem;

public sealed class CreateWorkItemCommandHandler(
    IWorkItemRepository workItemRepository,
    IUserExistenceChecker userExistenceChecker)
    : ICommandHandler<CreateWorkItemCommand, Guid>
{
    public async Task<Result<Guid>> Handle(
        CreateWorkItemCommand command,
        CancellationToken cancellationToken)
    {
        Result<WorkItem> result = WorkItem.Create(command.Name, command.AssigneeId);

        if (result.IsFailure)
        {
            return Result<Guid>.Failure(result.Error);
        }

        if (!await userExistenceChecker.ExistsAsync(command.AssigneeId, cancellationToken))
        {
            return Result<Guid>.Failure(WorkItemErrors.AssigneeNotFound(command.AssigneeId));
        }

        await workItemRepository.AddAsync(result.Value, cancellationToken);

        return Result<Guid>.Success(result.Value.Id);
    }
}
