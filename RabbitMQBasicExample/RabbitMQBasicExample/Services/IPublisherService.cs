using System;
using RabbitMQBasicExample.DTO;

namespace RabbitMQBasicExample.Services
{
    public interface IPublisherService
    {
        bool SendToQueue(List<QueueType> dto);
    }
}

