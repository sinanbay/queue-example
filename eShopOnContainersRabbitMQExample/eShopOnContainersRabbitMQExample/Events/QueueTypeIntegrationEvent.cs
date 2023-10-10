using EventBus.Events;

namespace eShopOnContainersRabbitMQExample.Events
{
    public record QueueTypeIntegrationEvent(Guid Id, string Message) : IntegrationEvent;
}

