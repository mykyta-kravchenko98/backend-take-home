using Microsoft.Extensions.DependencyInjection;

namespace BackendTakeHome.Modules.WorkItems.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddWorkItemsModule(this IServiceCollection services) => services;
}
