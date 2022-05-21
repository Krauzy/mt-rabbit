using GreenPipes;
using MassTransit;
using MassTransit.Definition;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Sample.Rabbit.Core.Extensions;

public static class MassTransitExtension
{
    public static void UseMassTransit(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            x.AddDelayedMessageScheduler();
            x.SetKebabCaseEndpointNameFormatter();

            x.UsingRabbitMq((context, config) =>
            {
                config.Host(configuration.GetConnectionString("RabbitMQ"));
                config.UseDelayedMessageScheduler();
                config.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter("dev", false));
                config.UseMessageRetry(retry =>
                {
                    retry.Interval(3, TimeSpan.FromSeconds(5));
                });
            });
        });
        // services.AddMassTransitHostedService();
    }
}

