using GreenPipes;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;
using Sample.Rabbit.Core.Events;

namespace Sample.Rabbit.Worker.Workers;

public class QueueUserUpdatedConsumer : IConsumer<UpdateUserEvent>
{
    private readonly ILogger<QueueUserUpdatedConsumer> _logger;

    public QueueUserUpdatedConsumer(ILogger<QueueUserUpdatedConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<UpdateUserEvent> context)
    {
        if (context.Message?.Name == "test") throw new ArgumentException("Invalid");

        var id = context.Message?.Id;
        var name = context.Message?.Name;

        _logger.LogInformation($"Receive user: {id} - {name}");
        return Task.CompletedTask;
    }
}

public class QueueUserUpdatedConsumerDefinition : ConsumerDefinition<QueueUserUpdatedConsumer> 
{
    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<QueueUserUpdatedConsumer> consumerConfigurator)
    {
        consumerConfigurator.UseMessageRetry(retry => retry.Interval(3, TimeSpan.FromSeconds(3)));
    }
}

