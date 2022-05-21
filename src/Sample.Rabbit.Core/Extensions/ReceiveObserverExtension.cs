using MassTransit;

namespace Sample.Rabbit.Core.Extensions;

public class ReceiveObserverExtension : IReceiveObserver
{
    public Task ConsumeFault<T>(ConsumeContext<T> context, TimeSpan duration, string comsumerType, Exception exception) where T : class
    {
        return Task.CompletedTask;
    }

    public Task PostConsume<T>(ConsumeContext<T> context, TimeSpan duration, string consumerType) where T : class
    {
        return Task.CompletedTask;
    }

    public Task PostReceive(ReceiveContext context)
    {
        return Task.CompletedTask;
    }

    public Task PreReceive(ReceiveContext context)
    {
        return Task.CompletedTask;
    }

    public Task ReceiveFault(ReceiveContext context, Exception exception)
    {
        return Task.CompletedTask;
    }
}
