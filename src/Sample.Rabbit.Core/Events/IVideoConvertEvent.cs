namespace Sample.Rabbit.Core.Events;

public interface IVideoConvertEvent
{
    string GroupId { get; }
    int Index { get; }
    int Count { get; }
    string Path { get; }
}
