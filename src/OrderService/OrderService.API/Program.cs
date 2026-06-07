using Carter;
using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderService.API.Extensions;
using OrderService.Application;
using OrderService.Application.Behaviors;
using OrderService.Application.Consumers;
using OrderService.Domain.Repositories;
using OrderService.Infrastructure.Persistence;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// EF Core - PostgreSQL
builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("OrderDb")));

// MediatR + Pipeline
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblyContaining<IAssemblyMarker>());

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<IAssemblyMarker>();

// Repositories
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

// MassTransit + RabbitMQ
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<InventoryReservedConsumer>();
    x.AddConsumer<InventoryFailedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMQ:Host"], "/", h =>
        {
            h.Username(builder.Configuration["RabbitMQ:Username"]!);
            h.Password(builder.Configuration["RabbitMQ:Password"]!);
        });

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

app.UseHttpsRedirection();
app.MapCarter();

app.Run();
