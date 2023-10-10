using System.Collections;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace RabbitMQBasicExample.Helper
{
    public class RabbitMQQueueHelper : IQueueHelper
    {
        private readonly string Url;
        private readonly string UserName;
        private readonly string Password;
        private readonly string QueueName;


        private IConnectionFactory _connectionFactory;

        public RabbitMQQueueHelper(string url, string userName, string password, string queueName)
        {
            this.Url = url;
            this.UserName = userName;
            this.Password = password;
            this.QueueName = queueName;
        }

        public IConnectionFactory GetFactory()
        {
            if (this._connectionFactory == null)
                this._connectionFactory = new ConnectionFactory()
                {
                    Uri = new Uri(this.Url),
                    UserName = this.UserName,
                    Password = this.Password

                };
            return this._connectionFactory;
        }

        //private IConnection _connection;
        //private IModel _channel;
        //public IConnection GetConnection()
        //{
        //    if (this._connection == null)
        //        this._connection = this.GetFactory().CreateConnection();
        //    return this._connection;
        //}
        //public IModel GetChannel()
        //{
        //    if (this._channel == null)
        //        this._channel = this.GetConnection().CreateModel();
        //    return this._channel;
        //}

        public bool SentToQueue(IList queueList)
        {
            bool result = true;
            using (var connection = this.GetFactory().CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: this.QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

                channel.BasicReturn += (sender, eventArgs) =>
                {
                    //var message = System.Text.Encoding.UTF8.GetString(eventArgs.Body.ToArray());
                    result = false;
                };

                foreach (var item in queueList)
                {
                    var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(item));
                    channel.BasicPublish("", this.QueueName, mandatory: true, basicProperties: null, body: body);
                }
            }
            return result;
        }

    }
}

