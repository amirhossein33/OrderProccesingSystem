using Carter;
using MediatR;
using OrderService.API.Endpoints.Dto;
using OrderService.Application.Commands;
using OrderService.Application.Queries;

namespace OrderService.API.Endpoints;

public class OrdersEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/orders");

        group.MapPost("/", async (CreateOrderRequest request, IMediator mediator, CancellationToken cancellationToken) =>
        {
            if (!Ulid.TryParse(request.ProductId, out var productId))
                return Results.BadRequest("Invalid ProductId format.");

            var command = new CreateOrderCommand(productId, request.Quantity);
            var result = await mediator.Send(command, cancellationToken);
            return Results.Created($"/orders/{result.OrderId}", result);
        });

        group.MapGet("/{id}", async (string id, IMediator mediator, CancellationToken cancellationToken) =>
        {
            if (!Ulid.TryParse(id, out var ulid))
                return Results.BadRequest("Invalid ID format.");

            var result = await mediator.Send(new GetOrderByIdQuery(ulid), cancellationToken);

            if (result is null)
                return Results.NotFound();

            return Results.Ok(result);
        });
    }
}

