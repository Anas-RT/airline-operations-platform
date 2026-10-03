export type AirlinePerformanceSevereDelayRate = {
  airlineName: string;
  otp15EligibleFlights: number;
  severeDelayFlights: number;
  severeDelayPct: number;
};

export type AirlineKpis = {
  completedFlights: number;
  otp15Rate: number;
  severeDelayRate: number;
  cancellationRate: number;
};
export type AirlineBenchmark = {
  targetAirlineOtp15Rate: number;
  networkOtp15Rate: number;
  targetAirlineSevereDelayRate: number;
  networkSevereDelayRate: number;
};
export type AirlineMonthlyOtp15Comparison = {
  month: string;
  networkOtp15Pct: number;
  targetAirlineOtp15Pct: number;
};
export type AirlineScorecardRow = {
  airlineCode: string;
  airlineName: string;
  completedFlights: number;
  otp15Rate: number;
  severeDelayRate: number;
  severeCases: number;
  cancellationRate: number;
  priorityInterpretation: string;
};
export type AirlineScorecardResponse = {
  totalCount: number;
  items: AirlineScorecardRow[];
};
