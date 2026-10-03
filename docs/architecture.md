# Architecture Notes

## Intended runtime flow

```text
React filter interaction
-> HTTP request
-> ASP.NET Core middleware
-> model binding
-> controller
-> service
-> repository/data access
-> PostgreSQL reporting table
-> response DTO
-> JSON
-> React rendering
```

## Responsibility split

| Layer | Responsibility |
|---|---|
| React frontend | Renders filters, loading states, errors, KPI cards, charts and tables |
| Controller | Understands HTTP and routes requests to the application operation |
| Service | Coordinates the use case and maps data to the response shape |
| Repository/data access | Runs parameterised database queries |
| PostgreSQL | Stores compact reporting tables |
| Python/DuckDB pipeline | Prepares and validates reporting tables from upstream analysis data |

## First-version data access decision

Use Dapper with Npgsql for the first version. The app is read-only and reporting-query focused, so explicit SQL is easier to explain and closer to the existing analytical work.