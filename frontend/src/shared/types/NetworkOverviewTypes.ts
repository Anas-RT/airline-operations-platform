export type NetworkOverviewFiltersOptions = {
  year: string[];
  airline: string[];
  month: string[];
  timeBand: string[];
};
export type NetworkOverviewFilters = {
  year: number | null;
  airline: string | null;
  month: string | null;
  timeBand: string | null;
};
export type NetworkOverviewFilterState = {
  year: string;
  airline: string;
  month: string;
  timeBand: string;
};
export type NetworkOverviewKpis = {
  otp15Pct: number;
  severeDelaySd60Pct: number;
  cancellationPct: number;
  diversionPct: number;
};
export type MonthlyTrend = {
  month: string;
  otp15Pct: number;
};

export type NetworkOverviewOutcomeMix = {
  totalFlights: number;
  otp15Flights: number;
  moderateDelayFlights: number;
  severeDelayFlights: number;
  divertedOrCancelledFlights: number;
};
export type AirlineOtp15PerformanceRate = {
  airlineName: string;
  otp15Rate: number;
};
