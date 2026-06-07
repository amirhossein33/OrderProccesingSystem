# Order Processing System

A mini e-commerce order processing system built with .NET 9 microservices architecture.

## Architecture

- **Clean Architecture** per service (Domain, Application, Infrastructure, Presentation)
- **CQRS** with MediatR for command/query separation
- **Microservices**: OrderService & InventoryService
- **MassTransit + RabbitMQ** for async inter-service communication
- **Entity Framework Core** with PostgreSQL (DB per service pattern)

## Project Structure

```
src/
├── Shared/
│   └── Shared.Contracts/          # Shared event contracts
├── OrderService/
│   ├── OrderService.Domain/       # Entities, Repositories interfaces
│   ├── OrderService.Application/  # Commands, Queries, Consumers (CQRS + MediatR)
│   ├── OrderService.Infrastructure/ # EF Core, Repository implementations
│   └── OrderService.API/          # REST API (Presentation layer)
└── InventoryService/
	├── InventoryService.Domain/       # Entities, Repositories interfaces
	├── InventoryService.Application/  # Consumers (MassTransit)
	├── InventoryService.Infrastructure/ # EF Core, Repository implementations
	└── InventoryService.API/          # REST API (Presentation layer)
```

## Prerequisites

- .NET 9 SDK
- Docker & Docker Compose
- RabbitMQ (via Docker)
- PostgreSQL (via Docker)

## Getting Started

### 1. Start Infrastructure

```bash
docker compose -f postgres.yml up -d
```

This starts:
- **PostgreSQL (Order DB)** on port `5433`
- **PostgreSQL (Inventory DB)** on port `5434`
- **RabbitMQ** on port `5672` (management UI: `http://localhost:15672`)
- **pgAdmin** on port `5050`

### 2. Run Services

Run both services (in separate terminals or use Visual Studio multiple startup projects):

```bash
# Terminal 1 - OrderService
dotnet run --project src/OrderService/OrderService.API

# Terminal 2 - InventoryService
dotnet run --project src/InventoryService/InventoryService.API
```

### 3. Test the Flow

**Create an Order** (will be processed asynchronously):
```bash
curl -X POST http://localhost:5100/orders \
  -H "Content-Type: application/json" \
  -d '{"productId": "PROD-001", "quantity": 5}'
```

**Get Order Status**:
```bash
curl http://localhost:5100/orders/{orderId}
```

**Check Inventory**:
```bash
curl http://localhost:5200/inventory/PROD-001
```

## Event Flow

1. `POST /orders` → OrderService saves order (status: **Pending**) → publishes `OrderCreatedEvent`
2. InventoryService receives `OrderCreatedEvent`:
   - If stock available → reserves stock → publishes `InventoryReservedEvent`
   - If insufficient → publishes `InventoryFailedEvent`
3. OrderService receives response event → updates order status to **Confirmed** or **Failed**

## Seed Data (Inventory)

| Product ID | Quantity |
|-----------|----------|
| PROD-001  | 100      |
| PROD-002  | 50       |
| PROD-003  | 200      |

## Configuration

| Service          | Port  | Database Port |
|-----------------|-------|---------------|
| OrderService    | 5100  | 5433          |
| InventoryService| 5200  | 5434          |
| RabbitMQ        | 5672  | -             |
| RabbitMQ UI     | 15672 | -             |
| pgAdmin         | 5050  | -             |
