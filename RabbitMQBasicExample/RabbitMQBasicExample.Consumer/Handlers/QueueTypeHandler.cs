using System;
using MediatR;

namespace RabbitMQBasicExample.Consumer.Handlers
{
	public class QueueTypeHandler : IRequestHandler<QueueTypeCommand, bool>
    {
        public QueueTypeHandler()
        {
        }

        public Task<bool> Handle(QueueTypeCommand request, CancellationToken cancellationToken)
        {
            bool result = false;
            try
            {
                Console.WriteLine("Id: " + request.QueueType.Id + " Message: " + request.QueueType.Message);
            }
            catch (Exception ex)
            {
                
            }
            return Task.FromResult(result);
        }
    }
}

