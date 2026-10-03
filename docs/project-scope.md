# Project Scope

## Objective

Build a small, polished, database-backed full-stack portfolio application that lets a user explore historical airline operational performance.

## MVP pages

1. Network Overview
2. Airline Performance
3. Delay Severity

## First vertical slice

The first slice proves the data-to-browser path:

```text
Reporting table -> ASP.NET Core API -> React request -> KPI cards -> monthly trend chart
```

## Excluded from MVP

- Authentication
- User accounts
- Real-time flight feeds
- Machine learning
- Microservices
- CQRS
- MediatR
- WebSockets
- Kubernetes
- Generic dashboard builder
- Sending raw flight-level data to the browser