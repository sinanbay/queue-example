using System;
using MediatR;
using RabbitMQBasicExample.DTO;

namespace RabbitMQBasicExample.Consumer.Handlers
{
	public class QueueTypeCommand : IRequest<bool>
    {
        public QueueType QueueType { get; set; }
    }
}

