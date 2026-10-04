import {
  BriefingCard,
  DashboardHeader,
  NarrativeCard,
  PageGuide,
  SectionHeader,
  TopBar,
  BubbleChart,
  HorizontalBarChart,
  MonthlyTrendCard,
  ChartCard,
  ProfileCard,
  PaginatedTable,
} from "../../shared/components";
import type {
  AirlineKpis,
  AirlinePerformanceSevereDelayRate,
  AirlineBenchmark,
  AirlineMonthlyOtp15Comparison,
  AirlineScorecardResponse,
} from "../../shared/types/AirlinePerformanceTypes";
import { useEffect, useState } from "react";
import styles from "./AirlinePerformancePage.module.css";
import {
  getAirlinePerformanceSevereDelayRate,
  getAirlineKpis,
  getAirlineBenchmark,
  getAirlineMonthlyOtp15Comparison,
  getAirlineScoreCard,
} from "../../shared/Services/AirlinePerformanceService";

export default function AirlinePerformancePage() {
  const [
    airlinePerformanceSevereDelayRate,
    setAirlinePerformanceSevereDelayRate,
  ] = useState<AirlinePerformanceSevereDelayRate[]>([]);
  const [airlineBenchmark, setAirlineBenchmark] =
    useState<AirlineBenchmark | null>(null);
  const [targetedAirlineKpis, setTargetedAirlineKpis] =
    useState<AirlineKpis | null>(null);
  const [airlineScoreCard, setAirlineScoreCard] =
    useState<AirlineScorecardResponse>({ totalCount: 0, items: [] });

  const [airlineMonthlyOtp15Comparison, setAirlineMonthlyOtp15Comparison] =
    useState<AirlineMonthlyOtp15Comparison[]>([]);
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(5);
  useEffect(() => {
    async function fetchData() {
      const airlinePerformanceSevereDelayRateData =
        await getAirlinePerformanceSevereDelayRate();

      setAirlinePerformanceSevereDelayRate(
        airlinePerformanceSevereDelayRateData,
      );
    }
    fetchData();
  }, []);

  const targetedAirline = airlinePerformanceSevereDelayRate
    .filter(
      (airline) =>
        airline.otp15EligibleFlights > 200000 && airline.severeDelayPct > 5,
    )
    .sort(
      (a, b) => b.severeDelayFlights - a.severeDelayFlights,
    )[0]?.airlineName;

  useEffect(() => {
    async function fetchTargetedAirlineData() {
      if (!targetedAirline) return;

      const [
        airlineKpisData,
        airlineBenchmarkData,
        airlineMonthlyOtp15ComparisonData,
      ] = await Promise.all([
        getAirlineKpis(targetedAirline),
        getAirlineBenchmark(targetedAirline),
        getAirlineMonthlyOtp15Comparison(targetedAirline),
      ]);

      setTargetedAirlineKpis(airlineKpisData);
      setAirlineBenchmark(airlineBenchmarkData);
      setAirlineMonthlyOtp15Comparison(airlineMonthlyOtp15ComparisonData);
    }

    fetchTargetedAirlineData();
  }, [targetedAirline]);

  useEffect(() => {
    async function fetchAirlineScoreCard(pageNumber: number, pageSize: number) {
      const airlineScoreCardData = await getAirlineScoreCard(
        pageNumber,
        pageSize,
      );
      setAirlineScoreCard(airlineScoreCardData);
    }
    fetchAirlineScoreCard(pageNumber, pageSize);
  }, [pageNumber, pageSize]);
  return (
    <div className={styles.pageContainer}>
      <header>
        <TopBar
          title="Airline Performance"
          description="Which airlines combine weak rates with meaningful operational scale?"
        />
      </header>
      <section>
        <DashboardHeader
          eyebrow="Dashboard 2 · Airline story"
          title="Airline Performance"
          description="Separate poor performance rate from operational scale, profile the selected carrier, and identify where deeper route or airport analysis would add value.  "
          tags={["Rate + volume", "Sortable ranking", "Carrier drill-down"]}
        />
      </section>
      <section>
        <PageGuide
          howToReadTitle="Start with portfolio position, then profile the selected carrier."
          howToReadDescription="The user first sees the market-wide shape, then the selected airline’s contribution and possible drivers. "
          storySteps={[
            {
              eyebrow: "Postion",
              title: "Who is weak?",
              description: "Use rate and volume simultaneously.",
            },
            {
              eyebrow: "Profile",
              title: "How does the carrier differ?",
              description:
                "Compare the selected airline with the network    benchmark.",
            },
            {
              eyebrow: "Investigation",
              title: "Where should analysis continue?",
              description:
                "Move toward airport, route and severe-delay detail.",
            },
          ]}
        />
      </section>
      <section>
        <BriefingCard
          briefingHeadline={`${targetedAirline} is not the weakest by rate, but its scale makes it one of the largest severe-delay contributors.`}
          briefingSummary="Spirit and Frontier show worse percentages, yet American’s much larger operation produces substantially more severe-delay cases. This is the kind of distinction a rate-only ranking would miss."
          insightPoints={[
            {
              number: 1,
              title: "Weak rate",
              description: "American sits below the network OTP15 benchmark.",
            },
            {
              number: 2,
              attentionStatus: true,
              title: "High impact",
              description:
                "Large volume turns a moderate rate gap into many cases.",
            },
            {
              number: 3,
              title: "Targeted follow-up",
              description:
                "Airport and route concentration should be checked next.",
            },
          ]}
        />
      </section>

      <section>
        <SectionHeader
          title="1. Which airlines combine weak rate with meaningful scale?"
          description="Bubble position shows rate and volume together."
        />
        <div className={styles.portfolioPanels}>
          <div className={styles.priorityMatrixPanel}>
            <div className={styles.header}>
              <h2>Completed-flight volume vs severe-delay rate.</h2>
              <p>
                Bubble size represents severe-delay flight count, highlighting
                operational impact alongside rate and scale.
              </p>
            </div>
            <BubbleChart
              data={airlinePerformanceSevereDelayRate.map((item) => ({
                x: item.otp15EligibleFlights,
                y: item.severeDelayPct,
                z: item.severeDelayFlights,
              }))}
              range={[500, 900]}
            />
            <div className={styles.priorityMatrixFooter}>
              {/* to be revisited and corrected */}
              <NarrativeCard
                attention={true}
                eyebrow="Priority signal"
                title="American sits in the high-volume, above-average-risk quadrant. "
                description="Spirit is a stronger rate outlier, but American creates greater absolute operational impact because of its scale. "
              />
            </div>
          </div>
          <div className={styles.interpretationPanel}>
            <div className={styles.header}>
              <h2>Portfolio interpretation</h2>
              <p>A simple decision frame for the analyst.</p>
            </div>
            <NarrativeCard
              eyebrow="High rate, lower volume"
              title="Spirit and Frontier"
              description="Potentially persistent performance issues, but smaller absolute case contribution."
            />

            <NarrativeCard
              attention={true}
              eyebrow="High volume, elevated rate"
              title="American Airlines"
              description="Highest priority for deeper analysis because scale and underperformance overlap."
            />

            <NarrativeCard
              eyebrow="High volume, stronger rate"
              title="Delta and Southwest"
              description="Still important for volume monitoring, but their rates require different interpretation."
            />

            <NarrativeCard
              eyebrow="Analyst question"
              title={`What is driving ${targetedAirline}’s contribution?"`}
              description="Break the carrier result down by month, airport, route and delay driver rather than treating the airline average as the final answer."
            />
          </div>
        </div>
      </section>
      <section>
        <SectionHeader
          title="2. How does the selected airline differ from the network?"
          description="The profile turns the portfolio signal into a specific carrier story."
        />

        <article>
          <ProfileCard
            eyebrow="Selected carrier profile"
            title={targetedAirline || ""}
            description={`${targetedAirline || ""} operates at substantial scale and trails the network OTP15 benchmark. Its severe-delay rate is above average, and the combination of scale and rate creates one of the largest absolute case volumes in the dataset.`}
            kpis={[
              {
                title: "Completed flights",
                value: targetedAirlineKpis?.completedFlights || 0,
              },
              {
                title: "OTP15 rate",
                rate: targetedAirlineKpis?.otp15Rate || 0,
              },
              {
                title: "Severe delay rate",
                rate: targetedAirlineKpis?.severeDelayRate || 0,
              },
              {
                title: "Cancellation rate",
                rate: targetedAirlineKpis?.cancellationRate || 0,
              },
            ]}
          />
        </article>
      </section>
      <section>
        <div className={styles.chartsGrid}>
          <ChartCard
            title={`${targetedAirline || ""} vs network benchmark`}
            description="The gap is modest in percentage points but substantial in absolute volume."
          >
            <div className={styles.benchmarkChart}>
              <HorizontalBarChart
                data={[
                  {
                    name: `${targetedAirline || ""} OTP15`,
                    percentage: airlineBenchmark?.targetAirlineOtp15Rate || 0,
                  },
                  {
                    name: "Network OTP15",
                    percentage: airlineBenchmark?.networkOtp15Rate || 0,
                  },
                  {
                    name: `${targetedAirline || ""} severe delay rate`,
                    percentage:
                      airlineBenchmark?.targetAirlineSevereDelayRate || 0,
                  },
                  {
                    name: "Network severe delay rate",
                    percentage: airlineBenchmark?.networkSevereDelayRate || 0,
                  },
                ]}
              />
            </div>
            <NarrativeCard
              eyebrow="Observed signal"
              title="Summer months show deterioration"
              description="Punctuality and severe-delay pressure worsen during the summer months."
            />
          </ChartCard>
          <ChartCard
            title={"Where the carrier weakness appears"}
            description="to be changed later"
          >
            <MonthlyTrendCard
              data={airlineMonthlyOtp15Comparison.map((item) => ({
                month: item.month,
                otp15Pct: item.targetAirlineOtp15Pct,
                comparisonOtp15Pct: item.networkOtp15Pct,
              }))}
              primaryLineName={targetedAirline}
              comparisonLineName="Network OTP15"
              comparisonArea={true}
              primaryDot={false}
            />
          </ChartCard>
        </div>
      </section>
      <section>
        <PaginatedTable
          data={airlineScoreCard?.items || []}
          pageNumber={pageNumber}
          onPageChange={setPageNumber}
          columns={[
            "Airline",
            "Completed Flights",
            "OTP15 %",
            "Severe Delay Rate %",
            "Severe Cases",
            "Cancellation Rate %",
            "Priority Interpretation",
          ]}
          pageSize={pageSize}
          totalCount={airlineScoreCard?.totalCount || 0}
        />
      </section>
      <section className={styles.nextSteps}>
        <NarrativeCard
          eyebrow="Observed signal"
          title="American has a moderate rate gap"
          description="Its performance is weaker than the network, but it is not the worst percentage."
        />

        <NarrativeCard
          eyebrow="Interpretation"
          title="Scale creates the operational impact"
          description="The carrier produces many more severe-delay cases than smaller rate outliers."
        />

        <NarrativeCard
          eyebrow="Next action"
          title="Open Delay Severity"
          description="Identify which delay bands and drivers create the largest impact."
        />
      </section>
    </div>
  );
}
