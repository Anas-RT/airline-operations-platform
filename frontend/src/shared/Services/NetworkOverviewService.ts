import type {
  NetworkOverviewKpis,
  MonthlyTrend,
  NetworkOverviewOutcomeMix,
  AirlineOtp15PerformanceRate,
  NetworkOverviewFiltersOptions,
  NetworkOverviewFilters,
} from "../types/NetworkOverviewTypes";

const NetworkOverviewBaseUrl =
  import.meta.env.VITE_API_BASE_URL + "/NetworkOverview";

function createFilterParams(filters: NetworkOverviewFilters) {
  const params = new URLSearchParams();

  if (filters.year) {
    params.append("year", filters.year.toString());
  }

  if (filters.airline) {
    params.append("airline", filters.airline);
  }

  if (filters.month) {
    params.append("month", filters.month);
  }

  if (filters.timeBand) {
    params.append("timeBand", filters.timeBand);
  }

  return params.toString();
}

export async function getKpis(
  filters: NetworkOverviewFilters,
): Promise<NetworkOverviewKpis> {
  const params = createFilterParams(filters);

  const response = await fetch(`${NetworkOverviewBaseUrl}/GetKpis?${params}`);

  if (!response.ok) {
    throw new Error("Failed to fetch KPIs");
  }

  return response.json();
}

export async function getGetOtp15Monthly(
  filters: NetworkOverviewFilters,
): Promise<MonthlyTrend[]> {
  const params = createFilterParams(filters);

  const response = await fetch(
    `${NetworkOverviewBaseUrl}/GetOtp15Monthly?${params}`,
  );

  if (!response.ok) {
    throw new Error("Failed to fetch monthly trend data");
  }

  return response.json();
}

export async function getGetFlightOutcomeMix(
  filters: NetworkOverviewFilters,
): Promise<NetworkOverviewOutcomeMix> {
  const params = createFilterParams(filters);

  const response = await fetch(
    `${NetworkOverviewBaseUrl}/GetFlightOutcomeMix?${params}`,
  );

  if (!response.ok) {
    throw new Error("Failed to fetch flight outcome mix data");
  }

  return response.json();
}

export async function getAirlineOtp15PerformanceRate(
  filters: NetworkOverviewFilters,
): Promise<AirlineOtp15PerformanceRate[]> {
  const params = createFilterParams(filters);

  const response = await fetch(
    `${NetworkOverviewBaseUrl}/GetAirlineOtp15PerformanceRate?${params}`,
  );

  if (!response.ok) {
    throw new Error("Failed to fetch airline OTP15 performance rate data");
  }

  return response.json();
}

export async function getNetworkOverviewFiltersOptions(): Promise<NetworkOverviewFiltersOptions> {
  const response = await fetch(
    `${NetworkOverviewBaseUrl}/GetNetworkOverviewFiltersOptions`,
  );

  if (!response.ok) {
    throw new Error("Failed to fetch network overview filters options");
  }

  return response.json();
}
