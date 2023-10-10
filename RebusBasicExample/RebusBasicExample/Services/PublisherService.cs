using System;
using Rebus.Bus;
using System.Text;
using RebusBasicExample.DTO;

namespace RebusBasicExample.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IBus bus;

        public PublisherService(IBus bus)
        {
            this.bus = bus;
        }

        public async Task<bool> SendToQueue(QueueType dto)
        {
            bool result = true;
            try
            {
                await bus.Publish(dto);
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
    }
}

