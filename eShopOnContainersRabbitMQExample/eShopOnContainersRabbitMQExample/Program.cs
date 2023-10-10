using eShopOnContainersRabbitMQExample.Services;
using Services.Common;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddServiceDefaults(builder.Configuration);

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

