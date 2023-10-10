using System;
using Rebus.Handlers;
using RebusBasicExample.DTO;

namespace RebusBasicExample.Consumer.Events
{
    public class QueueTypeEventHandler : IHandleMessages<QueueType>
    {

        public QueueTypeEventHandler()
        {
        }
        public Task Handle(QueueType request)
        {
            Console.WriteLine("Id: " + request.Id  + " Message:" + request.Message);
            return Task.CompletedTask;
        }
    }
}

