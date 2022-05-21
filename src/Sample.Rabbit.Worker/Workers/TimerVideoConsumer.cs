﻿using GreenPipes;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;
using MassTransit.JobService;
using Sample.Rabbit.Core.Events;

namespace Sample.Masstransit.Worker.Workers;

public class TimerVideoConsumer : IJobConsumer<IVideoConvertEvent>
{
    public async Task Run(JobContext<IVideoConvertEvent> context)
    {
        await Task.Delay(TimeSpan.FromSeconds(3));
    }
}

public class TimerVideoConsumerDefinition : ConsumerDefinition<TimerVideoConsumer>
{
    protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, IConsumerConfigurator<TimerVideoConsumer> consumerConfigurator)
    {
        consumerConfigurator.Options<JobOptions<IVideoConvertEvent>>(options =>
            options.SetRetry(r => r.Interval(3, TimeSpan.FromSeconds(30))).SetJobTimeout(TimeSpan.FromMinutes(1)).SetConcurrentJobLimit(10));
    }
}