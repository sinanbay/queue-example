using eShopOnContainersRabbitMQExample.Consumer.EventHandling;
using eShopOnContainersRabbitMQExample.Consumer.Events;
using EventBus.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Common;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServiceDefaults(builder.Configuration);

builder.Services.AddTransient<QueueTypeIntegrationEventHandler>();

var app = builder.Build();
var serviceProvider = app.Services;
var eventBus = app.Services.GetRequiredService<IEventBus>();
eventBus.Subscribe<QueueTypeIntegrationEvent, QueueTypeIntegrationEventHandler>();
Console.WriteLine("Mesaj bekleniyor...");

Console.ReadLine();