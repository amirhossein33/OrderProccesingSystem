using Carter;
using InventoryService.Domain.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;

namespace InventoryService.API.Endpoints;

public class InventoryEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/inventory");

        group.MapGet("/{productId}", async (string productId, IInventoryRepository inventoryRepository, CancellationToken cancellationToken) =>
        {
            if (!Ulid.TryParse(productId, out var ulid))
                return Results.BadRequest("Invalid ProductId format.");

            var item = await inventoryRepository.GetByProductIdAsync(ulid, cancellationToken);

            if (item is null)
                return Results.NotFound();

            return Results.Ok(new { ProductId = item.ProductId.ToString(), item.Quantity });
        });
    }
}
