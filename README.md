# Timeways API

Timeways is a calendar and event management REST API built with ASP.NET Core and .NET 10.

The project is being developed as a backend-focused portfolio project to build practical experience with ASP.NET Core, Entity Framework Core, PostgreSQL, authentication, testing, Docker, and Continuous Integration (CI).

## Goals

- Build a production-style ASP.NET Core Web API
- Practice backend architecture and separation of concerns
- Work with Entity Framework Core and PostgreSQL
- Implement authentication and authorization
- Support Portuguese and English
- Add recurring events, tags, and event reports
- Add automated testing
- Containerize the application with Docker
- Automate validation with GitHub Actions

## Technology Stack

- C#
- .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL
- JSON Web Token (JWT) Authentication
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

The domain model is currently being evolved from the initial project-management prototype into the calendar and event model described above.
