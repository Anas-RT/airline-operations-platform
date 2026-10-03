# Data Pipeline

Placeholder area for the lightweight data-preparation bridge between the existing GitHub airline performance analysis project and the application database.

No Python or SQL implementation has been written yet.

## Upstream source

The data pipeline should reuse prepared analytical outputs from `airline-performance-analysis`, including:

- `fact_flights.parquet`
- `dim_airlines.csv`
- `dim_airports.csv`
- `dim_cancellation_codes.csv`
- `dim_delay_driver.csv`

## Planned first outputs

- First reporting table for Network Overview
- Validation checks for KPI denominators
- Load step into PostgreSQL

## Boundary

Do not send raw flight-level data to the React frontend. The API should query compact reporting tables.