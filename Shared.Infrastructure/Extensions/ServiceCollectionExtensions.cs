﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure.Controller;

namespace Shared.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSharedInfrastructure(this IServiceCollection services, IConfiguration config)
    {

        services.AddControllers()
        .ConfigureApplicationPartManager(manager =>
        {
            manager.FeatureProviders.Add(new InternalControllerFeatureProvider());
        });
        return services;
    }

    public static IServiceCollection AddDatabaseContext<T>(this IServiceCollection services, IConfiguration config, string ConnectionStringName) where T : DbContext
    {
        var connectionString = config.GetConnectionString(ConnectionStringName); //("Default");
        services.AddMSSQL<T>(connectionString);
        return services;
    }

    private static IServiceCollection AddMSSQL<T>(this IServiceCollection services, string connectionString) where T : DbContext
    {
        services.AddDbContext<T>(m => m.UseSqlServer(connectionString, e => e.MigrationsAssembly(typeof(T).Assembly.FullName)));
        using var scope = services.BuildServiceProvider().CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<T>();
        dbContext.Database.Migrate();
        return services;
    }

}
