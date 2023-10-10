using System;
using RabbitMQBasicExample.DTO;
using RabbitMQBasicExample.Helper;

namespace RabbitMQBasicExample.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IQueueHelper queueHelper;

        public PublisherService(IQueueHelper queueHelper)
        {
            this.queueHelper = queueHelper;
        }

        public bool SendToQueue(List<QueueType> dto)
        {
            bool result = true;
            try
            {
                this.queueHelper.SentToQueue(dto);
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
    }
}

