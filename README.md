# Timeways API

Timeways is a calendar and event management REST API built with ASP.NET Core and .NET 10.

The project is a backend-focused portfolio project designed to build practical experience with ASP.NET Core, Entity Framework Core, PostgreSQL, authentication, validation, testing, Docker, and CI.

## Project Goals

- Build a production-style ASP.NET Core Web API
- Practice layered backend architecture and separation of concerns
- Model calendar and event-management domain concepts
- Work with Entity Framework Core and PostgreSQL
- Implement authentication and authorization
- Support Portuguese and English
- Support recurring events, tags, and event reporting
- Add automated tests
- Containerize the application with Docker
- Automate build and test validation with GitHub Actions

## Technology Stack

- C#
- .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL
- JWT Authentication
- FluentValidation
- Swagger / OpenAPI
- xUnit
- Serilog
- Docker
- GitHub Actions

## Architecture

Timeways follows a layered architecture:

```text
TimewaysAPI.Api
      ↓
TimewaysAPI.Application
      ↓
TimewaysAPI.Domain
      ↑
TimewaysAPI.Infrastructure
      ↓
  PostgreSQL
```

### Layers

- **Api** — HTTP endpoints, request/response handling, Swagger, and application startup.
- **Application** — application services and use-case orchestration.
- **Domain** — core business entities and domain rules, independent from infrastructure concerns.
- **Infrastructure** — Entity Framework Core, PostgreSQL persistence, and database configuration.

## Current Status

The initial ASP.NET Core solution, layered architecture, PostgreSQL/EF Core integration, generic entity base, and initial domain model are in place.

The project is currently being migrated from the original TaskFlow project-management prototype to its intended calendar/event-management domain. The remaining TaskFlow concepts are being replaced incrementally rather than kept as part of the final API.

## Development

The solution targets .NET 10 and uses PostgreSQL for persistence.

Before running the API, configure the `DefaultConnection` connection string in:

```text
src/TimewaysAPI.Api/appsettings.json
```

Swagger is enabled in the Development environment.

The repository also includes a GitHub Actions workflow that restores, builds, and tests the solution on pushes and pull requests targeting `main`.
