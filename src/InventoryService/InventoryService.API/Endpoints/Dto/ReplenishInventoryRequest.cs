namespace InventoryService.API.Endpoints.Dto;

public record ReplenishInventoryRequest(string ProductId, int Quantity);
