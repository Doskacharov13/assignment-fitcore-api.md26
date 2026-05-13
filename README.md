# FitCore API

## Overview

FitCore API is a gym management system built with ASP.NET Core 9 using Clean Architecture principles.

The system manages:
- Clients
- Memberships
- Workout classes
- Reservations
- Payments
- Trainers
- Rooms
- Visits

The project includes:
- REST API
- GraphQL
- SignalR notifications
- Entity Framework Core
- SQL Server
- Unit Tests
- Integration Tests

---

# Technologies

- ASP.NET Core 9
- Entity Framework Core 9
- SQL Server
- GraphQL (HotChocolate)
- SignalR
- Swagger
- xUnit
- FluentAssertions
- Moq

---

# Architecture

The solution follows Clean Architecture:

```text
FitCore.API
FitCore.Application
FitCore.Domain
FitCore.Infrastructure
FitCore.Tests.Unit
FitCore.Tests.Integration
```

---

# Features

## Clients
- Create clients
- Get all clients
- Active/inactive status

## Memberships
- Membership plans
- Membership validation
- Active memberships

## Workout Classes
- Create workout classes
- Cancel classes
- Capacity validation

## Reservations
- Reserve workout classes
- Prevent duplicate reservations
- Validate active membership
- Capacity checks

## Payments
- Register payments
- Payment validation

## Visits
- Register gym visits
- Validate memberships

## Notifications
SignalR notifications are sent when:
- Workout classes are cancelled
- Important events occur

---

# Database

The project uses SQL Server with Entity Framework Core migrations.

## Apply migrations

```powershell
Add-Migration InitialCreate
Update-Database
```

---

# Run the Project

## Start API

```powershell
dotnet run
```

Swagger:
```text
https://localhost:xxxx/swagger
```

GraphQL:
```text
https://localhost:xxxx/graphql
```

---

# Testing

## Run tests

```powershell
dotnet test
```

The project contains:
- Unit Tests
- Integration Tests

---

# GraphQL Example

```graphql
{
  clients {
    id
    fullName
    email
  }
}
```

---

# Author

Created for MD26 ASP.NET Core Assignment.