# TaskFlow API

TaskFlow is a project-management REST API built with ASP.NET Core and .NET 10.

The project is being developed as a backend-focused portfolio project to build practical experience with ASP.NET Core, Entity Framework Core, PostgreSQL, authentication, testing, Docker, and CI/CD.

## Goals

- Build a production-style ASP.NET Core Web API
- Practice backend architecture and separation of concerns
- Work with Entity Framework Core and PostgreSQL
- Implement authentication and authorization
- Add automated testing
- Containerize the application with Docker
- Automate validation with GitHub Actions

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

TaskFlow follows a layered architecture:

```text
TaskFlow.Api
      ↓
TaskFlow.Application
      ↓
TaskFlow.Domain
      ↑
TaskFlow.Infrastructure
      ↓
  PostgreSQL