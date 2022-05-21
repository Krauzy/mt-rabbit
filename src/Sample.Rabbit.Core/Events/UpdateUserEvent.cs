namespace Sample.Rabbit.Core.Events;

public class UpdateUserEvent
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime CreatedAt { get; set; }
}
