using MediatR;

namespace InventoryService.Application.Queries;

public record GetInventoryByProductIdQuery(Ulid ProductId) : IRequest<GetInventoryByProductIdResult?>;

public record GetInventoryByProductIdResult(string ProductId, int Quantity);
