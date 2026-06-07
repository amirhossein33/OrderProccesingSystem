using MediatR;
using InventoryService.Domain.Repositories;

namespace InventoryService.Application.Commands;

public class ReplenishInventoryCommandHandler : IRequestHandler<ReplenishInventoryCommand, ReplenishInventoryResult>
{
    private readonly IInventoryRepository _inventoryRepository;

    public ReplenishInventoryCommandHandler(IInventoryRepository inventoryRepository)
    {
        _inventoryRepository = inventoryRepository;
    }

    public async Task<ReplenishInventoryResult> Handle(ReplenishInventoryCommand request, CancellationToken cancellationToken)
    {
        var item = await _inventoryRepository.GetByProductIdAsync(request.ProductId, cancellationToken);

        if (item is null)
            throw new InvalidOperationException($"Inventory item for product {request.ProductId} not found.");

        item.Replenish(request.Quantity);

        await _inventoryRepository.UpdateAsync(item, cancellationToken);

        return new ReplenishInventoryResult(
            item.ProductId,
            item.Quantity);
    }
}
