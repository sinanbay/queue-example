using DTO;
using eShopOnContainersRabbitMQExample.Events;
using EventBus.Abstractions;

namespace eShopOnContainersRabbitMQExample.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IEventBus eventBus;

        public PublisherService(IEventBus eventBus)
        {
            this.eventBus = eventBus;
        }

        public async Task<bool> SendToQueue(QueueType dto)
        {
            bool result = true;
            try
            {
                QueueTypeIntegrationEvent obj = new QueueTypeIntegrationEvent(dto.Id, dto.Message);
                eventBus.Publish(obj);
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
    }
}

