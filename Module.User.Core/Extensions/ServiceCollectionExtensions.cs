using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Module.User.Core.Handlers;
using Module.User.Core.Helpers;
using System.Reflection;
using System.Text;

namespace Module.User.Core.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUserCore(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        services.AddScoped<IUserHelper, UserHelper>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        })
        .AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ByMX000OLlMQG6VVVp1OH7Xzyr7gHuw1qvUC5dcGt3SNM")),
                RoleClaimType = "mxur"
            };
        });
        return services;
    }
}