using MediatR;
using InventoryService.Domain.Repositories;

namespace InventoryService.Application.Queries;

public class GetInventoryByProductIdQueryHandler : IRequestHandler<GetInventoryByProductIdQuery, GetInventoryByProductIdResult?>
{
    private readonly IInventoryRepository _inventoryRepository;

    public GetInventoryByProductIdQueryHandler(IInventoryRepository inventoryRepository)
    {
        _inventoryRepository = inventoryRepository;
    }

    public async Task<GetInventoryByProductIdResult?> Handle(GetInventoryByProductIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _inventoryRepository.GetByProductIdAsync(request.ProductId, cancellationToken);

        if (item is null)
            return null;

        return new GetInventoryByProductIdResult(item.ProductId.ToString(), item.Quantity);
    }
}
