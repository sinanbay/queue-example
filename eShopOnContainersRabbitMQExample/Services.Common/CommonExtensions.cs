using EventBus;
using EventBus.Abstractions;
using EventBusRabbitMQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace Services.Common;

public static class CommonExtensions
{
    public static void AddServiceDefaults(this IServiceCollection services, IConfiguration config)
    {      
        services.AddEventBus(config);
    }
    public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
    {
        //  {
        //    "ConnectionStrings": {
        //      "EventBus": "..."
        //    },

        // {
        //   "EventBus": {
        //     "ProviderName": "ServiceBus | RabbitMQ",
        //     ...
        //   }
        // }

        // {
        //   "EventBus": {
        //     "ProviderName": "ServiceBus",
        //     "SubscriptionClientName": "eshop_event_bus"
        //   }
        // }

        // {
        //   "EventBus": {
        //     "ProviderName": "RabbitMQ",
        //     "SubscriptionClientName": "...",
        //     "UserName": "...",
        //     "Password": "...",
        //     "RetryCount": 1
        //   }
        // }
        var eventBusSection = configuration.GetSection("EventBus");
        if (!eventBusSection.Exists())
        {
            return services;
        }
        services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
        {
            var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

            var factory = new ConnectionFactory()
            {
                HostName = configuration.GetConnectionString("EventBus"),
                DispatchConsumersAsync = true
            };

            if (!string.IsNullOrEmpty(eventBusSection["UserName"]))
            {
                factory.UserName = eventBusSection["UserName"];
            }

            if (!string.IsNullOrEmpty(eventBusSection["Password"]))
            {
                factory.Password = eventBusSection["Password"];
            }

            var retryCount = Convert.ToInt32(eventBusSection.GetSection("RetryCount").Value);

            return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
        });

        services.AddSingleton<IEventBus, EventBusRabbitMQ.EventBusRabbitMQ>(sp =>
        {
            var subscriptionClientName = eventBusSection.GetSection("SubscriptionClientName").Value;
            var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
            var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ.EventBusRabbitMQ>>();
            var eventBusSubscriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();
            var retryCount = Convert.ToInt32(eventBusSection.GetSection("RetryCount").Value);

            return new EventBusRabbitMQ.EventBusRabbitMQ(rabbitMQPersistentConnection, logger, sp, eventBusSubscriptionsManager, subscriptionClientName, retryCount);
        });
        services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
        return services;
    }

}
