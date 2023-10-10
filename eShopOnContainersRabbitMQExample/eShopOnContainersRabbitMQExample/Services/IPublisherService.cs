using DTO;

namespace eShopOnContainersRabbitMQExample.Services
{
    public interface IPublisherService
    {
        Task<bool> SendToQueue(QueueType dto);
    }
}

