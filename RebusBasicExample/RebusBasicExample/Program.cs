using Microsoft.Extensions.DependencyInjection.Extensions;
using Rebus.Config;
using RebusBasicExample.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("PysDbConnection");

builder.Services.AddScoped<IPublisherService, PublisherService>();
var rebusSection = builder.Configuration.GetSection("Rebus");
builder.Services.AddRebus(configure =>
{
    var configurer = configure
        .Logging(l => l.ColoredConsole())
        .Transport(t => t.UseRabbitMqAsOneWayClient(rebusSection["Url"]));
    return configurer;
});

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

