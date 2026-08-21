using BackendTakeHome.Modules.Users.Infrastructure;
using BackendTakeHome.Modules.WorkItems.Infrastructure;
using FastEndpoints;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints(options =>
{
    options.Assemblies =
    [
        typeof(Program).Assembly,
        BackendTakeHome.Modules.Users.Presentation.AssemblyReference.Assembly,
        BackendTakeHome.Modules.WorkItems.Presentation.AssemblyReference.Assembly
    ];
});
builder.Services.AddUsersModule();
builder.Services.AddWorkItemsModule();

WebApplication app = builder.Build();

app.UseFastEndpoints();

app.Run();

public partial class Program;
