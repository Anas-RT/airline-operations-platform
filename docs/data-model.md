# Data Model Notes

## First reporting table

Recommended first table: `overview_performance_monthly`

## Grain

One row per reporting month, airline and time band, subject to validation against the upstream analysis dataset.

If the source data spans multiple years, the table should include a year or reporting-period field. This is an assumption to validate against the existing airline performance analysis repo.

## Store additive measures first

The table should store counts and totals that allow rates to be recomputed safely:

- Total flights
- Completed flights
- OTP15-eligible flights
- OTP15 flights
- Severe-delay flights
- Cancelled flights
- Diverted flights
- Total arrival-delay minutes where relevant

Rates should normally be calculated from numerators and denominators at query time or load time, not averaged from precomputed percentages.