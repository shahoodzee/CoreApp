using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.User.Core.Extensions;
using Module.User.Infrastructure.Extensions;

namespace Module.User;

public static class UserModuleExtension
{
    public static IServiceCollection AddUserModule(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddUserInfrastructure(configuration)
            .AddUserCore();
        return services;
    }
}
