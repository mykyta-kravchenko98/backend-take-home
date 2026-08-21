using BackendTakeHome.Modules.WorkItems.Application.WorkItems.GetWorkItemsByAssignee;
using FastEndpoints;
using MediatR;

namespace BackendTakeHome.Modules.WorkItems.Presentation.WorkItems.GetWorkItemsByAssignee;

public sealed class GetWorkItemsByAssigneeEndpoint(ISender sender)
    : Endpoint<GetWorkItemsByAssigneeRequest, IReadOnlyCollection<GetWorkItemsByAssigneeResponse>>
{
    public override void Configure()
    {
        Get("/api/work-items");
        AllowAnonymous();
    }

    public override async Task HandleAsync(
        GetWorkItemsByAssigneeRequest request,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new GetWorkItemsByAssigneeQuery(request.AssigneeId),
            cancellationToken);

        IReadOnlyCollection<GetWorkItemsByAssigneeResponse> response = result.Value
            .Select(workItem => new GetWorkItemsByAssigneeResponse(
                workItem.Id,
                workItem.Name,
                workItem.AssigneeId))
            .ToArray();

        await SendOkAsync(response, cancellationToken);
    }
}
