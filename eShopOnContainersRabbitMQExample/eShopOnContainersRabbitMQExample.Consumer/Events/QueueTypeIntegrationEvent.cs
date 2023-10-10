using System;
using EventBus.Events;

namespace eShopOnContainersRabbitMQExample.Consumer.Events
{
    public record QueueTypeIntegrationEvent(Guid Id, string Message) : IntegrationEvent;
}

