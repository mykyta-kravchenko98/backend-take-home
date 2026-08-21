using BackendTakeHome.Modules.Users.Contracts;
using BackendTakeHome.Modules.Users.Domain.Users;
using BackendTakeHome.Modules.Users.Infrastructure.Users;
using Microsoft.Extensions.DependencyInjection;

namespace BackendTakeHome.Modules.Users.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddUsersModule(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
            configuration.RegisterServicesFromAssembly(Application.AssemblyReference.Assembly));
        services.AddSingleton<IUserRepository, InMemoryUserRepository>();
        services.AddSingleton<IUserExistenceChecker, UserExistenceChecker>();

        return services;
    }
}
