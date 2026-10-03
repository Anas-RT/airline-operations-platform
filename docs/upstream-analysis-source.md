# Upstream Airline Performance Analysis Source

This application should reflect the existing GitHub airline performance analysis work rather than becoming a disconnected demo.

## Confirmed source relationship

- Analysis repository: `airline-performance-analysis`
- Software repository: `airline-operations-platform`
- Relationship: separate repositories, linked in documentation
- Framing: an airline operational analysis project later developed into a full-stack operational intelligence product

## Prepared outputs referenced by this app

- `fact_flights.parquet`
- `dim_airlines.csv`
- `dim_airports.csv`
- `dim_cancellation_codes.csv`
- `dim_delay_driver.csv`

## KPI definitions to preserve

- OTP15 means arrival no more than 15 minutes late.
- Severe delay means arrival delay of at least 60 minutes.
- OTP15 calculations use an eligibility flag.
- Primary delay driver is derived from air system, security, airline, late aircraft and weather delay fields.

## Boundary

This repository should not copy the full analytical project. The software app should consume prepared reporting tables derived from the analysis project.

## Assumption to validate

The exact public GitHub URL for `airline-performance-analysis` still needs to be added to this document and the root README.