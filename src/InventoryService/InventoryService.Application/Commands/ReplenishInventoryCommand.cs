using MediatR;

namespace InventoryService.Application.Commands;

public record ReplenishInventoryCommand(Ulid ProductId, int Quantity) : IRequest<ReplenishInventoryResult>;

public record ReplenishInventoryResult(Ulid ProductId, int NewQuantity);
