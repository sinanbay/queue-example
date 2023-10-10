using System;
using RebusBasicExample.DTO;

namespace RebusBasicExample.Services
{
    public interface IPublisherService
    {
        Task<bool> SendToQueue(QueueType dto);
    }
}

