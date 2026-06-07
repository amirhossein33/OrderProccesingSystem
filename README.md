# Order Processing System

A modern, scalable microservices-based order processing system built with .NET 9, featuring inter-service communication through RabbitMQ and comprehensive business logic validation.

## 📋 Table of Contents

- [Project Overview](#project-overview)
- [Architecture](#architecture)
- [Technology Stack](#technology-stack)
- [Project Structure](#project-structure)
- [Installed NuGet Packages](#installed-nuget-packages)
- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
- [Docker Compose](#docker-compose)
- [Services](#services)
- [Database](#database)
- [Message Queue](#message-queue)
- [API Documentation](#api-documentation)

---

## 🎯 Project Overview

The **Order Processing System** is a distributed system designed to handle order creation, validation, and inventory management across multiple microservices. The system uses event-driven architecture with message queuing to ensure reliable communication between services.

### Key Features

- ✅ Clean Architecture with separated concerns (Domain, Application, Infrastructure, API layers)
- ✅ Event-driven messaging using RabbitMQ via MassTransit
- ✅ CQRS pattern implementation with MediatR
- ✅ Input validation using FluentValidation
- ✅ PostgreSQL database with Entity Framework Core
- ✅ OpenAPI/Scalar.AspNetCore support for API documentation


---

## 🏗️ Architecture

The system follows a **microservices architecture** with two main services:

### Service Communication

- **Order Service** publishes events via RabbitMQ
- **Inventory Service** subscribes to Order events
- **Database Layer** uses PostgreSQL with Entity Framework Core
- **Logging Layer** integrates Elasticsearch (InventoryService)

---

## 🛠️ Technology Stack

- **Framework**: .NET 9.0
- **Architecture Pattern**: Clean Architecture + CQRS
- **Language**: C# with Nullable reference types and implicit usings
- **Web Framework**: ASP.NET Core
- **Database**: PostgreSQL with Entity Framework Core 9.0.7
- **Message Queue**: RabbitMQ via MassTransit 8.4.0
- **HTTP Routing**: Carter 8.2.1 (minimal API routing)
- **API Documentation**: OpenAPI/Scalar.AspNetCore 2.4.18
- **Validation**: FluentValidation 11.11.0
- **CQRS Pattern**: MediatR 12.4.1

---

## 📁 Project Structure

```
DotNetChallenge/
├── src/
│   ├── OrderService/
│   │   ├── OrderService.API/              # API layer with endpoints
│   │   ├── OrderService.Application/      # Business logic, commands, handlers
│   │   ├── OrderService.Domain/           # Domain entities and repositories
│   │   └── OrderService.Infrastructure/   # EF Core, database context
│   │
│   ├── InventoryService/
│   │   ├── InventoryService.API/          # API layer with endpoints
│   │   ├── InventoryService.Application/  # Business logic, event handlers
│   │   ├── InventoryService.Domain/       # Domain entities
│   │   └── InventoryService.Infrastructure/ # Database layer
│   │
│   └── Shared/
│       └── Shared.Contracts/              # Shared event contracts & DTOs
│
└── README.md
```

---

## 📦 Installed NuGet Packages

### OrderService.Application
- **FluentValidation 11.11.0** - Fluent API for validation rules
- **FluentValidation.DependencyInjectionExtensions 11.11.0** - DI extension for validation
- **MediatR 12.4.1** - CQRS pattern implementation
- **MassTransit 8.4.0** - Service-to-service messaging framework

### OrderService.API
- **Carter 8.2.1** - Minimal ASP.NET Core API routing
- **MassTransit.RabbitMQ 8.4.0** - RabbitMQ transport
- **MediatR 12.4.1** - CQRS commands and handlers
- **Microsoft.AspNetCore.OpenApi 9.0.7** - OpenAPI support
- **Scalar.AspNetCore 2.4.18** - Modern API documentation UI
- **Microsoft.EntityFrameworkCore.Design 9.0.7** - EF Core tooling

### OrderService.Infrastructure
- **Microsoft.EntityFrameworkCore 9.0.7** - ORM framework
- **Npgsql.EntityFrameworkCore.PostgreSQL 9.0.4** - PostgreSQL provider

### InventoryService.Application
- **MassTransit 8.4.0** - Event messaging
- **MediatR 12.4.1** - Message handling

### InventoryService.API
- **Carter 8.2.1** - API routing
- **Elastic.Clients.Elasticsearch 8.17.0** - Elasticsearch client
- **MassTransit.RabbitMQ 8.4.0** - RabbitMQ integration
- **MediatR 12.4.1** - CQRS
- **Microsoft.AspNetCore.OpenApi 9.0.7** - OpenAPI spec
- **Scalar.AspNetCore 2.4.18** - API docs UI
- **Serilog.AspNetCore 8.0.3** - ASP.NET Core logging
- **Serilog.Sinks.Elasticsearch 10.0.0** - Elasticsearch sink
- **Microsoft.EntityFrameworkCore.Design 9.0.7** - EF tooling

### InventoryService.Infrastructure
- **Microsoft.EntityFrameworkCore 9.0.7** - ORM
- **Npgsql.EntityFrameworkCore.PostgreSQL 9.0.4** - PostgreSQL support

---

## 📋 Prerequisites

- .NET 9.0 SDK or higher
- PostgreSQL 12+ (database server)
- RabbitMQ 3.12+ (message broker)
- Visual Studio 2022+ or Visual Studio Code

---

## 🚀 Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/amirhossein33/OrderProccesingSystem.git
cd DotNetChallenge
```

### 2. Configure Environment

Create an appsettings.json file in each API project:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=5432;Database=OrderServiceDb;User Id=postgres;Password=your_password;"
  },
  "RabbitMQ": {
    "Host": "localhost",
    "Port": 5672,
    "Username": "guest",
    "Password": "guest"
  },
  "Elasticsearch": {
    "Uri": "http://localhost:9200"
  }
}
```

### 3. Install Dependencies

```bash
dotnet restore
```

### 4. Apply Migrations

```bash
# For OrderService
cd src/OrderService/OrderService.API
dotnet ef database update --project ../OrderService.Infrastructure

# For InventoryService
cd ../../InventoryService/InventoryService.API
dotnet ef database update --project ../InventoryService.Infrastructure
```

### 5. Run Services

```bash
# Terminal 1 - Order Service
cd src/OrderService/OrderService.API
dotnet run

# Terminal 2 - Inventory Service
cd src/InventoryService/InventoryService.API
dotnet run
```

---

## 🐳 Docker Compose

### Ports

| Container | Description | Host Port | Container Port |
|---|---|---|---|
| postgres_order | PostgreSQL – Order DB | 5435 | 5432 |
| postgres_inventory | PostgreSQL – Inventory DB | 5434 | 5432 |
| rabbitmq | RabbitMQ broker | 5672 | 5672 |
| rabbitmq | RabbitMQ management UI | 15672 | 15672 |
| postgres_pgadmin | pgAdmin UI | 5051 | 80 |

### Run

```bash
docker-compose up -d
```
---

## 🏢 Services

### Order Service

**Purpose**: Handles order creation and management

- **Port**: https://localhost:7001
- **API Documentation**: https://localhost:7001/api-docs
- **Key Components**:
  - CreateOrderCommand: CQRS command for order creation
  - CreateOrderCommandHandler: Publishes OrderCreatedEvent
  - Order: Domain entity

### Inventory Service

**Purpose**: Manages product inventory and responds to order events

- **Port**: https://localhost:7002
- **API Documentation**: https://localhost:7002/api-docs
- **Key Components**:
  - Event consumers for OrderCreatedEvent
  - Inventory update logic

---

## 🗄️ Database

The system uses **Entity Framework Core 9.0.7** with **PostgreSQL**.

**Features**:
- Code-first migrations
- LINQ query provider
- DbContext pattern

### Schema Overview

Orders table contains order records with ProductId, Quantity, and Status.
Inventory table maintains product stock information.

---

## 📨 Message Queue

The system uses **MassTransit 8.4.0** with **RabbitMQ**.

**Event Flow**:
1. Order Service publishes OrderCreatedEvent
2. RabbitMQ routes to subscribers
3. Inventory Service updates stock

---

## 📚 API Documentation

Interactive API docs via **Scalar.AspNetCore**:
- Order Service: https://localhost:7100/scalar
- Inventory Service: https://localhost:7200/scalar

---

## 🔍 Key Patterns

### Clean Architecture
Domain → Application → Infrastructure → API

### CQRS
Commands via MediatR handlers
Queries through repositories

### Event-Driven
Services communicate asynchronously via RabbitMQ

---

## 🛠️ Development

```bash
# Build
dotnet build
```

---

## 👤 Repository

[amirhossein33/OrderProccesingSystem](https://github.com/amirhossein33/OrderProccesingSystem)
