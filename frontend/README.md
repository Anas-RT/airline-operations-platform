# Frontend

Vite React TypeScript scaffold for the Airline Operations Intelligence Platform.

Template code exists from the Vite generator. The airline-domain Overview page, filters, KPI cards and chart have not been implemented yet.

## Planned first feature files

- `src/app/router.tsx`
- `src/app/queryClient.ts`
- `src/features/overview/OverviewPage.tsx`
- `src/features/overview/OverviewFilters.tsx`
- `src/features/overview/KpiCards.tsx`
- `src/features/overview/MonthlyTrendChart.tsx`
- `src/features/overview/overviewApi.ts`
- `src/features/overview/overviewTypes.ts`

## First frontend goal

Call `GET /api/overview`, then render loading, error, empty and success states for four KPI cards and one monthly trend chart.