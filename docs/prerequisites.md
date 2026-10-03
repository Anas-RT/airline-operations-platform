# Prerequisites and Installed Libraries

## Local tools detected

- .NET SDK: 10.0.103
- Node.js: v24.13.1
- npm: 11.10.1
- Python: 3.13.9

## Backend scaffold and packages

Scaffolded:

- ASP.NET Core Web API targeting `net10.0`
- Controller-based template
- xUnit test project targeting `net10.0`
- Solution file: `backend/AirlineOperations.slnx`

Installed NuGet packages:

- `Dapper` 2.1.79
- `Npgsql` 10.0.3
- `Microsoft.AspNetCore.OpenApi` 10.0.3 from the template
- `Microsoft.AspNetCore.Mvc.Testing` 10.0.10
- xUnit test template packages: `xunit`, `xunit.runner.visualstudio`, `Microsoft.NET.Test.Sdk`, `coverlet.collector`

Decision: Dapper + Npgsql is the first-version data-access choice. EF Core has not been installed, because the project brief asks not to learn both at the beginning and the first slice is reporting-query focused.

## Frontend scaffold and packages

Scaffolded:

- Vite React TypeScript app

Installed npm dependencies:
React Router deferred: `react-router-dom` was removed on 2026-08-03 because npm audit reported high severity vulnerabilities in the available `7.x` line and the app is not using routing yet. Add routing back when the first feature genuinely needs it and a non-vulnerable package line is available.

- `react`
- `react-dom`
- `react-router-dom`
- `@tanstack/react-query`
- `recharts`

Installed npm dev dependencies include:

- `typescript`
- `vite`
- `@vitejs/plugin-react`
- `vitest`
- `@testing-library/react`
- `@testing-library/jest-dom`
- `@testing-library/user-event`
- `jsdom`
- `oxlint`

## Data and database prerequisites

- PostgreSQL is represented by `docker-compose.yml` for local development.
- Python/DuckDB pipeline files are placeholders only.
- The upstream GitHub `airline-performance-analysis` repository remains the source of KPI definitions and prepared analytical data.

## Warnings to review

- npm audit was cleaned on 2026-08-03 by removing unused `react-router-dom`; `npm audit --audit-level=high` reported 0 vulnerabilities afterwards.
- NuGet reported a high-severity advisory involving `Microsoft.OpenApi` 2.0.0 during package restore. Review with `dotnet list package --vulnerable` before implementation.