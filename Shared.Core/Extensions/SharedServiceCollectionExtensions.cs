using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System.Xml.Linq;

namespace Shared.Core.Extensions;

public static class SharedServiceCollectionExtensions
{
    public static IServiceCollection AddSharedCore(this IServiceCollection services)
    {
        //services.AddMassTransit(config =>
        //{
        //    config.UsingRabbitMq((context, cfg) =>
        //    {
        //        cfg.Host(new Uri("rabbitmq://localhost"), h =>
        //        {
        //            h.Username("guest");
        //            h.Password("guest");
        //        });
        //    });
        //});
        return services;
    }
}
