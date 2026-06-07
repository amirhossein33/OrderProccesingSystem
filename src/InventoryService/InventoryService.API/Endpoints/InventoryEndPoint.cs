using Carter;
using MediatR;
using InventoryService.Domain.Repositories;
using InventoryService.Application.Commands;
using InventoryService.Application.Queries;
using InventoryService.API.Endpoints.Dto;
using Microsoft.AspNetCore.Http.HttpResults;

namespace InventoryService.API.Endpoints;

public class InventoryEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/inventory");

        group.MapGet("/{productId}", async (string productId, IMediator mediator, CancellationToken cancellationToken) =>
        {
            if (!Ulid.TryParse(productId, out var ulid))
                return Results.BadRequest("Invalid ProductId format.");

            var result = await mediator.Send(new GetInventoryByProductIdQuery(ulid), cancellationToken);

            if (result is null)
                return Results.NotFound();

            return Results.Ok(result);
        });

        group.MapPost("/replenish", async (ReplenishInventoryRequest request, IMediator mediator, CancellationToken cancellationToken) =>
        {
            if (!Ulid.TryParse(request.ProductId, out var productId))
                return Results.BadRequest("Invalid ProductId format.");

            if (request.Quantity <= 0)
                return Results.BadRequest("Quantity must be greater than zero.");

            var command = new ReplenishInventoryCommand(productId, request.Quantity);

            try
            {
                var result = await mediator.Send(command, cancellationToken);
                return Results.Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return Results.NotFound(new { Message = ex.Message });
            }
        });
    }
}

