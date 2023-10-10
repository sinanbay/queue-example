using System;
using eShopOnContainersRabbitMQExample.Consumer.Events;
using EventBus.Abstractions;

namespace eShopOnContainersRabbitMQExample.Consumer.EventHandling
{
	public class QueueTypeIntegrationEventHandler : IIntegrationEventHandler<QueueTypeIntegrationEvent>
    {
        public QueueTypeIntegrationEventHandler()
        {
        }

        public async Task Handle(QueueTypeIntegrationEvent @event)
        {
            Console.WriteLine("Id: " + @event.Id + " Message:" + @event.Message);
        }
    }
}