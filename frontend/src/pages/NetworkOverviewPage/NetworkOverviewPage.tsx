import { useState, useEffect } from "react";
import { useSearchParams } from "react-router-dom";

/*=============================styles import============================== */
import styles from "./NetworkOverviewPage.module.css";

/*=============================types import============================== */
import type {
  NetworkOverviewKpis,
  MonthlyTrend,
  NetworkOverviewOutcomeMix,
  AirlineOtp15PerformanceRate,
  NetworkOverviewFiltersOptions,
  NetworkOverviewFilters,
} from "../../shared/types/NetworkOverviewTypes";
/*=============================utils import============================== */

import { formatName } from "../../Utils/formatters";
/*=============================services import============================== */

import {
  getGetFlightOutcomeMix,
  getGetOtp15Monthly,
  getKpis,
  getAirlineOtp15PerformanceRate,
  getNetworkOverviewFiltersOptions,
} from "../../shared/Services/NetworkOverviewService";

/*=============================components import============================== */
import {
  TopBar,
  DashboardHeader,
  InsightPoint,
  KpiCard,
  MonthlyTrendCard,
  OutcomeMixChart,
  ChartLegend,
  HorizontalBarChart,
  NarrativeCard,
  FilterBar,
  PageGuide,
} from "../../shared/components";

export default function NetworkOverviewPage() {
  const fills = ["#7565E8", "#B8AEF3", "#F2A65A", "#FF6B6B"];
  //=============================state variables==============================
  //++++++++++++++++++++to work on ++++++++++++++++++++++++++++++++
  const [isLoading, setIsLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [searchParams, setSearchParams] = useSearchParams();
  //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
  const [filtersToApply, setFiltersToApply] = useState<NetworkOverviewFilters>({
    year: null,
    airline: null,
    month: null,
    timeBand: null,
  });
  const [filters, setFilters] = useState<NetworkOverviewFiltersOptions>({
    year: [],
    airline: [],
    month: [],
    timeBand: [],
  });

  const [kpis, setKpis] = useState<NetworkOverviewKpis | null>(null);
  const [monthlyTrendData, setMonthlyTrendData] = useState<MonthlyTrend[]>([]);
  const [flightOutcomeMixObject, setFlightOutcomeMixObject] =
    useState<NetworkOverviewOutcomeMix | null>(null);
  const [airlineOtp15PerformanceRateData, setAirlineOtp15PerformanceRateData] =
    useState<AirlineOtp15PerformanceRate[]>([]);
  useEffect(() => {
    async function fetchFilterOptions() {
      const filtersData = await getNetworkOverviewFiltersOptions();
      setFilters(filtersData);
    }

    fetchFilterOptions();
  }, []);
  useEffect(() => {
    async function fetchData() {
      const [kpiData, monthlyData, outcomeMixObject, airlineData] =
        await Promise.all([
          getKpis(filtersToApply),
          getGetOtp15Monthly(filtersToApply),
          getGetFlightOutcomeMix(filtersToApply),
          getAirlineOtp15PerformanceRate(filtersToApply),
        ]);

      setKpis(kpiData);
      setMonthlyTrendData(monthlyData);
      setFlightOutcomeMixObject(outcomeMixObject);
      setAirlineOtp15PerformanceRateData(airlineData);
    }

    fetchData();
  }, [filtersToApply]);

  const percentageCalculation = (value: number, total: number) =>
    total ? (value / total) * 100 : 0;
  const outcomeMixData = Object.entries(flightOutcomeMixObject ?? {})
    .filter(([key]) => key !== "totalFlights")
    .map(([key, value], index) => ({
      name: formatName(key),
      value: Number(value),
      percentage: percentageCalculation(
        value,
        flightOutcomeMixObject?.totalFlights ?? 0,
      ),
      fill: fills[index % fills.length],
    }));
  if (!kpis) {
    return <p>Loading...</p>;
  }

  return (
    <div className={styles.pageContainer}>
      <header>
        <TopBar
          title="Network Overview"
          description="How is the network performing, and where should attention move next?"
        />
      </header>
      <div>
        <DashboardHeader
          eyebrow="Dashboard 1 · Network story"
          title="Network Overview"
          description="Establish the network baseline, identify the period driving deterioration, and move the analyst toward the airlines and delay patterns that deserve deeper investigation. "
          tags={[
            "Read-only historical analysis",
            "Database-backed filters",
            "Executive summary first",
          ]}
        />
      </div>

      {/* ==================================Page Guide Section=================================== */}
      <section>
        <PageGuide
          howToReadTitle="Start with the message, then follow the evidence."
          howToReadDescription="The page is deliberately sequenced so the user does not have to interpret every chart independently."
          storySteps={[
            {
              eyebrow: "Signal",
              title: "What changed?",
              description:
                "Read the network-level KPI direction and headline narrative.",
            },
            {
              eyebrow: "Concentration",
              title: "Where did it happen?",
              description:
                "Use the trend and airline comparison to locate the weakness.",
            },
            {
              eyebrow: "Next question",
              title: "What should be investigated?",
              description:
                "Move from overview into airline and severe-delay analysis.",
            },
          ]}
        />
      </section>
      {/* ==================================Briefing Section=================================== */}
      <article className={styles.briefingSection}>
        <div className={styles.briefingContent}>
          <p className={styles.eyebrow}>Executive briefing</p>
          <h3 className={styles.headline}>
            Punctuality is broadly stable, but summer disruption creates a clear
            operational weakness.
          </h3>
          <p className={styles.summary}>
            Network OTP15 is 78.4%. June is the weakest month, with severe-delay
            pressure rising at the same time. The overall result is not
            explained by one small carrier: several high-volume airlines
            contribute meaningful case volume, so the next step is to separate
            poor rate from large operational scale.{" "}
          </p>
        </div>
        <div className={styles.briefingPoints}>
          <InsightPoint
            number={1}
            title="Baseline remains acceptable"
            description="Most flights arrive within the OTP15 threshold."
          />
          <InsightPoint
            number={2}
            attentionStatus={true}
            title="June needs investigation"
            description="The weakest punctuality and highest severe-delay pressure coincide."
          />
          <InsightPoint
            number={3}
            title="Volume changes the story"
            description="High-volume carriers matter even when their rate is close to average."
          />
        </div>
      </article>
      {/* ==================================Filter Section=================================== */}
      <div className={styles.filtersSection}>
        <FilterBar
          filterOptions={filters}
          onApply={(value) => {
            const filtersToApply: NetworkOverviewFilters = {
              year: value.year === "All" ? null : Number(value.year),
              airline: value.airline === "All" ? null : value.airline,
              month: value.month === "All" ? null : value.month,
              timeBand: value.timeBand === "All" ? null : value.timeBand,
            };
            setSearchParams({
              year: String(value.year),
              airline: value.airline ?? "",
              month: value.month ?? "",
              timeBand: value.timeBand ?? "",
            });
            setFiltersToApply(filtersToApply);
          }}
        />
      </div>
      {/* ================================Kpi section===================================== */}
      <div className={styles.kpisSection}>
        {/* ===================================Kpi Header================================== */}
        <div className={styles.kpisHeader}>
          <h2>1.What Changed</h2>
          <p>Network health before drilling into the causes.</p>
        </div>

        {/* ===================================Kpi Content================================== */}
        <div className={styles.kpisContent}>
          <KpiCard
            title="Network OTP15"
            rate={kpis.otp15Pct}
            description="The percentage of flights arriving within 15 minutes of schedule."
          />
          <KpiCard
            title="Severe delay pressure"
            rate={kpis.severeDelaySd60Pct}
            description="The percentage of flights arriving more than 60 minutes late."
          />
          <KpiCard
            title="Cancellation rate"
            rate={kpis.cancellationPct}
            description="The percentage of flights cancelled."
          />
          <KpiCard
            title="Diversion rate"
            rate={kpis.diversionPct}
            description="The percentage of flights diverted."
          />
        </div>
      </div>
      {/* ===================================Weakness Section================================== */}
      <div className={styles.weaknessSection}>
        <div className={styles.weaknessHeader}>
          <h2>2.Where did it happen</h2>
          <p>Identify the airlines and time periods driving deterioration.</p>
        </div>
        <div className={styles.weaknessGrid}>
          <div className={styles.monthlyTrendCard}>
            <h3>Monthly OTP15 Trend</h3>
            <MonthlyTrendCard data={monthlyTrendData} />
          </div>
          {/* ===================================Outcome Mix Content================================== */}

          <div className={styles.outcomeMixCard}>
            <div className={styles.outcomeMixCardHeader}>
              <h3>Outcome mix</h3>
              <p>The distribution of flight outcomes per year.</p>
            </div>
            {/******************************************************************************************* */}
            <div className={styles.outcomeMixCardContent}>
              {flightOutcomeMixObject ? (
                <OutcomeMixChart
                  data={outcomeMixData}
                  total={flightOutcomeMixObject.totalFlights}
                  totalLabel="flights"
                />
              ) : (
                <p>Loading outcome mix...</p>
              )}
              {/******************************************************************************************* */}
              <div className={styles.outcomeMixCardLegend}>
                <ChartLegend data={outcomeMixData} />
              </div>
              {/******************************************************************************************* */}
            </div>
            <div className={styles.outcomeMixCardFooter}>
              <p className={styles.eyebrow}>Why this matters</p>
              <h3 className={styles.title}>
                The annual rate hides a short but important deterioration.
              </h3>
              <p className={styles.description}>
                {/* ---------------------To be revised----------------- */}
                The annual outcome mix is dominated by OTP15 flights, but the
                summer months show a clear deterioration in punctuality and
                severe-delay pressure. The next step is to identify the airlines
                driving this weakness.
              </p>
            </div>
          </div>
          <div className={styles.airlineComparisonCard}>
            <h3>Airline OTP15 Performance Rate</h3>
            <p>
              Rate identifies weak performers, but volume determines operational
              weight.
            </p>
            <HorizontalBarChart
              data={airlineOtp15PerformanceRateData.map((item) => ({
                name: item.airlineName,
                percentage: item.otp15Rate,
              }))}
            />
          </div>
          <div className={styles.interpretationCard}>
            <h3>Analytical interpretation</h3>
            <p>
              The airline OTP15 performance rate provides insights into the
              punctuality of different airlines. By analyzing this data, we can
              identify patterns and areas for improvement.
            </p>
            {/* ---------------------To be revised----------------- */}
            <div className={styles.findings}>
              <NarrativeCard
                eyebrow="finding 1"
                title="The problem is seasonal."
                description="June and July carry more disruption than September and October, so annual averages alone are not enough. "
              />
              <NarrativeCard
                eyebrow="finding 2"
                title="The problem is not owned by one carrier."
                description="Spirit has the weakest rate, but American and Southwest create more cases because they operate at much greater scale. "
              />
              <NarrativeCard
                attention={true}
                eyebrow="finding 3"
                title="Compare rate and volume together."
                description="The Airline Performance dashboard separates high-rate outliers from high-volume contributors. "
              />
            </div>
          </div>
        </div>
        <section className={styles.nextSteps}>
          <NarrativeCard
            eyebrow="Observed signal"
            title="Summer months show deterioration"
            description="The network line identifies a concentrated seasonal weakness. "
          />
          <NarrativeCard
            eyebrow="Interpretation"
            title="Rate alone is incomplete"
            description="Large carriers create operational impact even with moderate rates. "
          />
          <NarrativeCard
            eyebrow="Next action"
            title="Open Airline Performance"
            description="Prioritise carriers where weak rate and high volume overlap. "
          />
        </section>
      </div>
    </div>
  );
}
