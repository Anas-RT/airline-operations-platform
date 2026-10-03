import type {
  AirlineBenchmark,
  AirlineKpis,
  AirlineMonthlyOtp15Comparison,
  AirlinePerformanceSevereDelayRate,
  AirlineScorecardResponse,
} from "../types/AirlinePerformanceTypes";

const AirlinePerformanceApiBaseUrl =
  import.meta.env.VITE_API_BASE_URL + "/AirlinePerformance";

export async function getAirlinePerformanceSevereDelayRate(): Promise<
  AirlinePerformanceSevereDelayRate[]
> {
  const response = await fetch(
    `${AirlinePerformanceApiBaseUrl}/GetAirlinePerformanceSevereDelayRate`,
  );
  if (!response.ok) {
    throw new Error("Failed to fetch airline performance severe delay rate");
  }
  return response.json();
}

export async function getAirlineKpis(
  targetAirline: string,
): Promise<AirlineKpis> {
  const params = new URLSearchParams();
  params.set("targetAirline", targetAirline);

  const response = await fetch(
    `${AirlinePerformanceApiBaseUrl}/GetAirlineKpis?${params.toString()}`,
  );
  if (!response.ok) {
    throw new Error("Failed to fetch airline KPIs");
  }
  return response.json();
}

export async function getAirlineBenchmark(
  targetAirline: string,
): Promise<AirlineBenchmark> {
  const params = new URLSearchParams();
  params.set("targetAirline", targetAirline);

  const response = await fetch(
    `${AirlinePerformanceApiBaseUrl}/GetAirlineBenchmark?${params.toString()}`,
  );
  if (!response.ok) {
    throw new Error("Failed to fetch airline benchmark");
  }
  return response.json();
}

export async function getAirlineMonthlyOtp15Comparison(
  targetAirline: string,
): Promise<AirlineMonthlyOtp15Comparison[]> {
  const params = new URLSearchParams();
  params.set("targetAirline", targetAirline);

  const response = await fetch(
    `${AirlinePerformanceApiBaseUrl}/GetAirlineMonthlyOtp15Comparison?${params.toString()}`,
  );
  if (!response.ok) {
    throw new Error("Failed to fetch airline monthly OTP15 comparison");
  }
  return response.json();
}

export async function getAirlineScoreCard(
  pageNumber: number,
  pageSize: number,
): Promise<AirlineScorecardResponse> {
  const params = new URLSearchParams();
  params.set("pageNumber", pageNumber.toString());
  params.set("pageSize", pageSize.toString());

  const response = await fetch(
    `${AirlinePerformanceApiBaseUrl}/GetAirlineScoreCard?${params.toString()}`,
  );
  if (!response.ok) {
    throw new Error("Failed to fetch airline scorecard");
  }
  return response.json();
}
