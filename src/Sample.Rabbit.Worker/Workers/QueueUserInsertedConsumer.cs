using GreenPipes;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;
using MassTransit.Metadata;
using Sample.Rabbit.Core.Events;
using System.Diagnostics;

namespace Sample.Rabbit.Worker.Workers;

public class QueueUserInsertedConsumer : IConsumer<InsertUserEvent>
{
    private readonly ILogger<QueueUserInsertedConsumer> _logger;

    public QueueUserInsertedConsumer(ILogger<QueueUserInsertedConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<InsertUserEvent> context)
    {
        var timer = Stopwatch.StartNew();

        try
        {
            var id = context.Message.Id;
            var name = context.Message.Name;
            var email = context.Message.Email;

            await context.Publish(new SendEmailEvent { Email = email });

            _logger.LogInformation($"Receive user: {id} - {name}");
            await context.NotifyConsumed(timer.Elapsed, TypeMetadataCache<InsertUserEvent>.ShortName);
        }
        catch(Exception ex)
        {
            await context.NotifyFaulted(timer.Elapsed, TypeMetadataCache<InsertUserEvent>.ShortName, ex);
        }
    }
}

public class QueueUserConsumerDefinition : ConsumerDefinition<QueueUserInsertedConsumer>
{
    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<QueueUserInsertedConsumer> consumerConfigurator)
    {
        consumerConfigurator.UseMessageRetry(retry => retry.Interval(3, TimeSpan.FromSeconds(3)));
    }
}

