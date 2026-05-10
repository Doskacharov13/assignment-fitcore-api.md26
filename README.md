# FitCore Gym & Membership API

FitCore is an ASP.NET Core Web API project for managing a fitness center, memberships, trainers, workout classes, reservations, visits, and payments.

The system is designed using a layered architecture and includes:
- REST API
- GraphQL API
- SignalR real-time notifications
- Entity Framework Core
- SQL Server database
- Unit tests
- Integration tests

---

# Technologies

## Backend
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- SignalR
- GraphQL (HotChocolate)

## Testing
- xUnit
- Moq
- FluentAssertions

## Documentation
- Swagger / OpenAPI
- XML Documentation

---

# Architecture

The project follows a clean layered architecture.

```text
FitCore.sln
├── FitCore.API
├── FitCore.Application
├── FitCore.Domain
├── FitCore.Infrastructure
├── FitCore.Tests.Unit
└── FitCore.Tests.Integration
