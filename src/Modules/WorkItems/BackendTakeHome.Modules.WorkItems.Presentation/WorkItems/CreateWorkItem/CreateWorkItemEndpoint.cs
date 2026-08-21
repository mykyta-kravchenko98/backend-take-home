using BackendTakeHome.Common.Domain;
using BackendTakeHome.Modules.WorkItems.Application.WorkItems.CreateWorkItem;
using FastEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BackendTakeHome.Modules.WorkItems.Presentation.WorkItems.CreateWorkItem;

public sealed class CreateWorkItemEndpoint(ISender sender)
    : Endpoint<CreateWorkItemRequest, CreateWorkItemResponse>
{
    public override void Configure()
    {
        Post("/api/work-items");
        AllowAnonymous();
    }

    public override async Task HandleAsync(
        CreateWorkItemRequest request,
        CancellationToken cancellationToken)
    {
        var result = await sender.Send(
            new CreateWorkItemCommand(request.Name, request.AssigneeId),
            cancellationToken);

        if (result.IsFailure)
        {
            if (result.Error.Type == ErrorType.NotFound)
            {
                await SendNotFoundAsync(cancellationToken);
                return;
            }

            await SendResultAsync(Results.Problem(
                statusCode: StatusCodes.Status400BadRequest,
                title: "Work item creation failed",
                detail: result.Error.Description,
                extensions: new Dictionary<string, object?>
                {
                    ["code"] = result.Error.Code
                }));
            return;
        }

        await SendAsync(new CreateWorkItemResponse(result.Value), 201, cancellationToken);
    }
}
