using BackendTakeHome.Common.Application.Messaging;
using BackendTakeHome.Common.Domain;
using BackendTakeHome.Modules.WorkItems.Domain.WorkItems;

namespace BackendTakeHome.Modules.WorkItems.Application.WorkItems.GetWorkItemsByAssignee;

public sealed class GetWorkItemsByAssigneeQueryHandler(IWorkItemRepository workItemRepository)
    : IQueryHandler<GetWorkItemsByAssigneeQuery, IReadOnlyCollection<WorkItemResponse>>
{
    public async Task<Result<IReadOnlyCollection<WorkItemResponse>>> Handle(
        GetWorkItemsByAssigneeQuery query,
        CancellationToken cancellationToken)
    {
        IReadOnlyCollection<WorkItem> workItems =
            await workItemRepository.GetByAssigneeIdAsync(query.AssigneeId, cancellationToken);

        IReadOnlyCollection<WorkItemResponse> response = workItems
            .Select(workItem => new WorkItemResponse(workItem.Id, workItem.Name, workItem.AssigneeId))
            .ToArray();

        return Result<IReadOnlyCollection<WorkItemResponse>>.Success(response);
    }
}
