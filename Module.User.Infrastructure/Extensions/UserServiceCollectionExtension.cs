using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Module.User.Core.Abstraction;
using Module.User.Core.Entities;
using Module.User.Infrastructure.Persistence;
using Shared.Infrastructure.Extensions;

namespace Module.User.Infrastructure.Extensions;

public static class UserServiceCollectionExtension
{
    public static IServiceCollection AddUserInfrastructure(this IServiceCollection services, IConfiguration config)
    {

        services
            .AddDatabaseContext<UserDbContext>(config, "User")
            .AddScoped<IUserDbContext>(provider => provider.GetService<UserDbContext>());

        services.AddIdentity<ApplicationUser, ApplicationRole>(opt =>
        {
            opt.SignIn.RequireConfirmedEmail = true;
            opt.Lockout.AllowedForNewUsers = true;
            opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(20);
            opt.Lockout.MaxFailedAccessAttempts = 3;
        }).AddEntityFrameworkStores<UserDbContext>()
        .AddDefaultTokenProviders();

        return services;
    }
}
