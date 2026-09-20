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

### Layers

- **Api** — HTTP endpoints, request/response handling, Swagger, and application startup.
- **Application** — application services and use-case orchestration.
- **Domain** — core business entities and domain rules, independent from infrastructure concerns.
- **Infrastructure** — Entity Framework Core, PostgreSQL persistence, and database configuration.

## Current Status

The initial ASP.NET Core solution, layered architecture, PostgreSQL/EF Core integration, generic entity base, and initial domain foundation are in place.

The current development focus is the calendar and event-management domain, including events, users, recurring events, tags, and reporting.
