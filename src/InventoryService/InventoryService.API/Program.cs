using Carter;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using InventoryService.API.Extensions;
using InventoryService.Application.Consumers;
using InventoryService.Domain.Repositories;
using InventoryService.Infrastructure.Persistence;
using Scalar.AspNetCore;
using Shared.Contracts.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<InventoryDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("InventoryDb")));

builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();

builder.Services.AddMediatR(cfg => 
    cfg.RegisterServicesFromAssembly(typeof(InventoryService.Application.IAssemblyMarker).Assembly));

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderCreatedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMQ:Host"], "/", h =>
        {
            h.Username(builder.Configuration["RabbitMQ:Username"]!);
            h.Password(builder.Configuration["RabbitMQ:Password"]!);
        });
        cfg.UseMessageRetry(r => r.Exponential(3,
    TimeSpan.FromSeconds(1),
    TimeSpan.FromSeconds(10),
    TimeSpan.FromSeconds(2)));

        cfg.ConfigureEndpoints(context);

    });
});
builder.Services.AddCarter();
builder.Services.AddOpenApi();

var app = builder.Build();

await app.ApplyDbMigrationsAsync();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseGlobalExceptionHandler();
app.UseHttpsRedirection();
app.MapCarter();

app.Run();
