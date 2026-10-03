# Repository Structure

This is the expected working structure for `airline-operations-platform`, excluding generated folders such as `node_modules`, `bin`, `obj`, `dist` and `.git`.

Hidden `.gitkeep` files may exist in empty folders so Git can preserve the planned structure.

```text
airline-operations-platform/
|-- README.md
|-- .gitignore
|-- .env.example
|-- docker-compose.yml
|
|-- docs/
|   |-- project-scope.md
|   |-- architecture.md
|   |-- data-model.md
|   |-- api-contracts.md
|   |-- prerequisites.md
|   |-- repository-structure.md
|   `-- upstream-analysis-source.md
|
|-- frontend/
|   |-- package.json
|   |-- package-lock.json
|   |-- index.html
|   |-- vite.config.ts
|   |-- tsconfig.json
|   |-- tsconfig.app.json
|   |-- tsconfig.node.json
|   |-- README.md
|   |
|   |-- public/
|   |
|   `-- src/
|       |-- main.tsx
|       |-- App.tsx
|       |-- App.css
|       |-- index.css
|       |
|       |-- app/
|       |   |-- router.tsx
|       |   `-- queryClient.ts
|       |
|       |-- features/
|       |   |-- overview/
|       |   |   |-- OverviewPage.tsx
|       |   |   |-- OverviewFilters.tsx
|       |   |   |-- KpiCards.tsx
|       |   |   |-- MonthlyTrendChart.tsx
|       |   |   |-- overviewApi.ts
|       |   |   `-- overviewTypes.ts
|       |   |
|       |   |-- airlines/
|       |   `-- delaySeverity/
|       |
|       `-- shared/
|           |-- api/
|           |-- components/
|           |-- hooks/
|           |-- types/
|           `-- utils/
|
|-- backend/
|   |-- AirlineOperations.slnx
|   |-- README.md
|   |
|   |-- src/
|   |   `-- AirlineOperations.Api/
|   |       |-- AirlineOperations.Api.csproj
|   |       |-- Program.cs
|   |       |-- appsettings.json
|   |       |-- appsettings.Development.json
|   |       |-- AirlineOperations.Api.http
|   |       |
|   |       |-- Controllers/
|   |       |
|   |       |-- Features/
|   |       |   |-- Overview/
|   |       |   |   |-- OverviewController.cs
|   |       |   |   |-- OverviewService.cs
|   |       |   |   |-- IOverviewService.cs
|   |       |   |   |-- OverviewRepository.cs
|   |       |   |   |-- IOverviewRepository.cs
|   |       |   |   |-- OverviewFilter.cs
|   |       |   |   `-- OverviewResponse.cs
|   |       |   |
|   |       |   |-- Airlines/
|   |       |   `-- DelaySeverity/
|   |       |
|   |       |-- Data/
|   |       |   |-- DatabaseConnectionFactory.cs
|   |       |   `-- Sql/
|   |       |
|   |       `-- Common/
|   |           |-- Errors/
|   |           |-- Middleware/
|   |           |-- Pagination/
|   |           `-- Validation/
|   |
|   `-- tests/
|       `-- AirlineOperations.Api.Tests/
|           |-- AirlineOperations.Api.Tests.csproj
|           `-- UnitTest1.cs
|
`-- data-pipeline/
    |-- README.md
    |-- requirements.txt
    |
    |-- scripts/
    |   |-- build_reporting_tables.py
    |   |-- validate_outputs.py
    |   `-- load_database.py
    |
    `-- sql/
        |-- overview.sql
        |-- airline_performance.sql
        `-- delay_severity.sql
```

## Current cleanup notes

The default ASP.NET `WeatherForecast` sample files and Visual Studio local `.vs` folders were removed so the repo matches this intended project structure more closely.