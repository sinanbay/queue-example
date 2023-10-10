namespace RabbitMQBasicExample.DTO;
public record QueueType
{
    public Guid Id { get; set; }
    public string Message { get; set; }

    public QueueType(Guid id, string message)
    {
        Id = id;
        Message = message;
    }
}

