
using System.Reflection;
using System.Text;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQBasicExample.Consumer.Handlers;
using RabbitMQBasicExample.DTO;

var serviceProvider = new ServiceCollection();
var environmentName = !string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")) ? "." + Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") : "";
var configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile($"appsettings{environmentName}.json", optional: true)
            .Build();
serviceProvider.AddMediatR(cfg => { cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()); });
var app = serviceProvider.BuildServiceProvider();

var mediator = app.GetRequiredService<IMediator>();

var rabbitMQSection = configuration.GetSection("RabbitMQ");
var factory = new ConnectionFactory()
{
    Uri = new Uri(rabbitMQSection["Url"]),
    UserName = rabbitMQSection["UserName"],
    Password = rabbitMQSection["Password"],
};
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();
channel.QueueDeclare(rabbitMQSection["QueueName"], durable: true, exclusive: false, autoDelete: false, arguments: null);
var consumer = new EventingBasicConsumer(channel);
consumer.Received += (sender, e) =>
{
    var message = Encoding.UTF8.GetString(e.Body.ToArray());
    Console.WriteLine(message);
    QueueType? dto = JsonConvert.DeserializeObject<QueueType>(message);
    if (dto != null)
    {
        var command = new QueueTypeCommand { QueueType = dto };
        var result = mediator.Send(command).Result;
    }
};
channel.BasicConsume(queue: rabbitMQSection["QueueName"], autoAck: true, consumer: consumer);

Console.WriteLine("Consumer started.");
Console.ReadLine();