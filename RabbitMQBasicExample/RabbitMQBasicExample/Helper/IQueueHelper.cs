using System;
using System.Collections;

namespace RabbitMQBasicExample.Helper
{
	public interface IQueueHelper
	{
        bool SentToQueue(IList queueList);
    }
}

