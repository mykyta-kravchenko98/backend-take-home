using BackendTakeHome.Modules.Users.Application.Users.CreateUser;
using FastEndpoints;
using MediatR;

namespace BackendTakeHome.Modules.Users.Presentation.Users.CreateUser;

public sealed class CreateUserEndpoint(ISender sender) : Endpoint<CreateUserRequest, CreateUserResponse>
{
    public override void Configure()
    {
        Post("/api/users");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var result = await sender.Send(new CreateUserCommand(request.Username), cancellationToken);

        if (result.IsFailure)
        {
            await SendAsync(new CreateUserResponse(Guid.Empty), 400, cancellationToken);
            return;
        }

        await SendAsync(new CreateUserResponse(result.Value), 201, cancellationToken);
    }
}
