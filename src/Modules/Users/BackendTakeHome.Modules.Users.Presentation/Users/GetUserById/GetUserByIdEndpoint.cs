using BackendTakeHome.Modules.Users.Application.Users.GetUserById;
using FastEndpoints;
using MediatR;

namespace BackendTakeHome.Modules.Users.Presentation.Users.GetUserById;

public sealed class GetUserByIdEndpoint(ISender sender)
    : Endpoint<GetUserByIdRequest, GetUserByIdResponse>
{
    public override void Configure()
    {
        Get("/api/users/{id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetUserByIdRequest request, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new GetUserByIdQuery(request.Id), cancellationToken);

        if (result.IsFailure)
        {
            await SendNotFoundAsync(cancellationToken);
            return;
        }

        UserResponse user = result.Value;
        await SendOkAsync(new GetUserByIdResponse(user.Id, user.Username), cancellationToken);
    }
}
