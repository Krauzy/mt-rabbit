using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Sample.Rabbit.Core.Extensions;

public static class OpenTelemetryExtension
{
    public static void UseOpenTelemetry(this IServiceCollection services, AppSettings appSettings)
    {
        services.AddOpenTelemetryTracing(telemetry =>
        {
            telemetry.AddMassTransitInstrumentation();
            var resourceBuilder = ResourceBuilder.CreateDefault()
                .AddService(appSettings?.DistributedTracing?.Jaeger?.ServiceName);

            telemetry
                .SetResourceBuilder(resourceBuilder)
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .SetSampler(new AlwaysOffSampler())
                .AddJaegerExporter(jaegerOptions =>
                {
                    jaegerOptions.AgentHost = appSettings?.DistributedTracing?.Jaeger?.Host;
                    jaegerOptions.AgentPort = appSettings?.DistributedTracing?.Jaeger?.Port ?? 0;
                });
        });
    }
}
