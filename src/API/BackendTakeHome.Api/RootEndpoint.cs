using FastEndpoints;

namespace BackendTakeHome.Api;

public sealed class RootEndpoint : EndpointWithoutRequest<string>
{
    public override void Configure()
    {
        Get("/");
        AllowAnonymous();
    }

    public override Task HandleAsync(CancellationToken cancellationToken) =>
        SendAsync("BackendTakeHome API", cancellation: cancellationToken);
}
