# Timeways API

A calendar and event management REST API built with **ASP.NET Core / .NET 10**.

Built as a portfolio project to develop practical backend experience while transitioning from Front-End to Full Stack development.

## Features

* User registration and login
* JWT authentication and authorization
* Event CRUD
* Event ownership
* Request validation
* Global exception handling
* PostgreSQL persistence
* EF Core migrations
* Swagger/OpenAPI
* Unit and integration tests
* Docker Compose

## Tech Stack

* **C# / .NET 10**
* **ASP.NET Core**
* **Entity Framework Core**
* **PostgreSQL**
* **JWT**
* **FluentValidation**
* **xUnit**
* **Testcontainers**
* **Docker**
* **GitHub Actions**

## Architecture

```text
src/
├── TimewaysAPI.Api
├── TimewaysAPI.Application
├── TimewaysAPI.Domain
└── TimewaysAPI.Infrastructure

tests/
└── TimewaysAPI.UnitTests
```

Simple layered architecture with a focus on keeping the project practical and avoiding unnecessary abstractions.

## Running Locally

### Requirements

* .NET 10 SDK
* PostgreSQL 17

Configure the database connection and JWT settings using ASP.NET Core configuration or User Secrets.

Then:

```bash
dotnet restore
dotnet ef database update --project src/TimewaysAPI.Infrastructure --startup-project src/TimewaysAPI.Api
dotnet run --project src/TimewaysAPI.Api
```

Swagger:

```text
http://localhost:5074/swagger
```

## Docker

The repository includes Docker Compose for running the API and PostgreSQL together.

```bash
docker compose up --build
```

API:

```text
http://localhost:8080
```

Stop the containers:

```bash
docker compose down
```

## API

### Authentication

```text
POST /api/auth/register
POST /api/auth/login
```

### Events

```text
GET    /api/Events
GET    /api/Events/{id}
POST   /api/Events
PUT    /api/Events/{id}
DELETE /api/Events/{id}
```

### Health

```text
GET /api/health
```

Authenticated users can only access and modify their own events.

## Testing

```bash
dotnet test
```

Integration tests use **Testcontainers** with a real PostgreSQL instance.

**6/6 tests passing.**
