namespace Sample.Rabbit.Core.Events;

public class InsertUserEvent
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime CreatedAt { get; set; }
}

