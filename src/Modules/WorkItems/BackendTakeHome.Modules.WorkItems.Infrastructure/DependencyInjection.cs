using BackendTakeHome.Modules.WorkItems.Domain.WorkItems;
using BackendTakeHome.Modules.WorkItems.Infrastructure.WorkItems;
using Microsoft.Extensions.DependencyInjection;

namespace BackendTakeHome.Modules.WorkItems.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddWorkItemsModule(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
            configuration.RegisterServicesFromAssembly(Application.AssemblyReference.Assembly));
        services.AddSingleton<IWorkItemRepository, InMemoryWorkItemRepository>();

        return services;
    }
}
