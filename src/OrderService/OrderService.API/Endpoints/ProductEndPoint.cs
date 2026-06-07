using Carter;
using MediatR;
using OrderService.Application.Queries;

namespace OrderService.API.Endpoints;

public class ProductEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/products");

        group.MapGet("/", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetAllProductsQuery());
            return Results.Ok(result);
        });
    }
}
