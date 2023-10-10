using Microsoft.Extensions.DependencyInjection.Extensions;
using RabbitMQBasicExample.Helper;
using RabbitMQBasicExample.Services;

var builder = WebApplication.CreateBuilder(args);

var rabbitMQSection = builder.Configuration.GetSection("RabbitMQ");
builder.Services.AddScoped<IQueueHelper>(sp =>
{
    return new RabbitMQQueueHelper(rabbitMQSection["Url"], rabbitMQSection["UserName"], rabbitMQSection["Password"], rabbitMQSection["QueueName"]);
});
builder.Services.AddScoped<IPublisherService, PublisherService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

