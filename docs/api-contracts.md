# API Contracts

## Initial endpoint

`GET /api/overview`

## Purpose

Return compact Overview KPI and monthly trend data for the first vertical slice.

## Optional query parameters

| Parameter | Type | Rule |
|---|---|---|
| airline | string | Optional airline code from the airline dimension |
| month | integer | Optional value from 1 to 12 |
| timeBand | string | Optional time-band value supported by the reporting table |

## Response responsibilities

The response should include:

- Applied filters
- Four KPI values: OTP15 rate, severe-delay rate, cancellation rate, diversion or disruption rate
- Monthly trend points
- No flight-level raw records

## Empty behaviour

A valid request with no matching records should return an empty/zero-data response that the frontend can distinguish from an API failure.

## Validation behaviour

Invalid filters should return a controlled validation error rather than silently producing misleading results.