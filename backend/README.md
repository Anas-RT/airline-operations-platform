# Backend

ASP.NET Core Web API scaffold for the Airline Operations Intelligence Platform.

Template code exists from the .NET generator. The airline-domain Overview endpoint, PostgreSQL query and response mapping have not been implemented yet.

## Scaffolded

- `AirlineOperations.slnx`
- `src/AirlineOperations.Api/AirlineOperations.Api.csproj`
- `tests/AirlineOperations.Api.Tests/AirlineOperations.Api.Tests.csproj`

## Planned first feature files

- `src/AirlineOperations.Api/Features/Overview/OverviewController.cs`
- `src/AirlineOperations.Api/Features/Overview/OverviewService.cs`
- `src/AirlineOperations.Api/Features/Overview/IOverviewService.cs`
- `src/AirlineOperations.Api/Features/Overview/OverviewRepository.cs`
- `src/AirlineOperations.Api/Features/Overview/IOverviewRepository.cs`
- `src/AirlineOperations.Api/Features/Overview/OverviewFilter.cs`
- `src/AirlineOperations.Api/Features/Overview/OverviewResponse.cs`

## First backend goal

Create a controller-based `GET /api/overview` endpoint that receives filter DTOs through query parameters, calls a service, uses Dapper/Npgsql to run a parameterised PostgreSQL query and returns a compact response DTO.