# Airline Operations Intelligence Platform

A full-stack operational intelligence platform for exploring airline, airport, route and delay performance across approximately 5.8 million historical flights.

## Current status

The repository has been scaffolded with:

- Vite React TypeScript frontend
- ASP.NET Core Web API backend
- xUnit backend test project
- Planned feature-based folders for the first Overview vertical slice
- Documentation and Obsidian planning vault

No airline-domain feature implementation has been written yet. Template files from Vite and ASP.NET Core may still exist and should be replaced as the first vertical slice is built.

## Relationship to the analysis project

This software repository is separate from, but linked to, the airline performance analysis repository.

Upstream analytical source:

- GitHub repository: `airline-performance-analysis` (exact URL to confirm)
- Prepared outputs referenced by this app: `fact_flights.parquet`, `dim_airlines.csv`, `dim_airports.csv`, `dim_cancellation_codes.csv`, `dim_delay_driver.csv`
- KPI definitions reused here: OTP15 rate, severe-delay rate, cancellation rate, diversion or disruption rate, completed flights, average arrival delay, severe-delay volume and total severe-delay minutes

The application should reflect the prepared analytical data and KPI definitions from that GitHub analysis project. It should not copy the whole analysis repo or restart data discovery from zero.

## First build milestone

```text
One reporting table
-> one database query
-> GET /api/overview
-> one React API request
-> four KPI cards
-> one monthly trend chart
```

## Stack scaffolded

- Frontend: React, TypeScript, Vite, Recharts, TanStack Query, Vitest, React Testing Library. React Router is planned but deferred until a non-vulnerable package line is selected.
- Backend: ASP.NET Core Web API, C#, controller-based endpoints, OpenAPI, xUnit
- Data access prerequisite: Dapper with Npgsql for PostgreSQL
- Data preparation area: Python/DuckDB-oriented `data-pipeline` folder

## Start here

- Planning vault: `C:\Users\kados\OneDrive\Desktop\folders\Obsidian notes\Airline Operations Intelligence Platform`
- Repo structure notes: `docs/repository-structure.md`
- Prerequisites and install log: `docs/prerequisites.md`
- Upstream source notes: `docs/upstream-analysis-source.md`