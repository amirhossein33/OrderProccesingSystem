namespace OrderService.API.Endpoints.Dto;

public record CreateOrderRequest(string ProductId, int Quantity);
