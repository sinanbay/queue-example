
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rebus.Activation;
using Rebus.Config;
using RebusBasicExample.Consumer.Events;
using RebusBasicExample.DTO;

var serviceProvider = new ServiceCollection();
var environmentName = !string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")) ? "." + Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") : "";
var configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile($"appsettings{environmentName}.json", optional: true)
            .Build();
//var connectionString = configuration.GetConnectionString("DbConnection");
//serviceProvider.AddEntityFrameworkNpgsql().AddDbContext<DbContext>(opt => opt.UseNpgsql(connectionString));
var app = serviceProvider.BuildServiceProvider();


var rebusSection = configuration.GetSection("Rebus");

using (var activator = new BuiltinHandlerActivator())
{
    activator.Register(() => new QueueTypeEventHandler());

    var subscriber = Configure.With(activator)
        .Transport(t => t.UseRabbitMq(rebusSection["Url"], rebusSection["QueueName"]))
        .Start();

    await subscriber.Subscribe<QueueType>();

    Console.WriteLine("Press ENTER to quit");

    Console.ReadLine();
}