using InventoryService.Domain.Entities;

namespace InventoryService.Domain.Repositories;

public interface IInventoryRepository
{
    Task<InventoryItem?> GetByProductIdAsync(Ulid productId, CancellationToken cancellationToken = default);
    Task UpdateAsync(InventoryItem item, CancellationToken cancellationToken = default);
    Task AddAsync(InventoryItem item, CancellationToken cancellationToken = default);
}
