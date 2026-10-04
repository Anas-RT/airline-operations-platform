import {
  DashboardHeader,
  InsightPoint,
  KpiCard,
  PageGuide,
  SectionHeader,
  TopBar,
} from "../../shared/components";
import styles from "./DelaySeverityPage.module.css";

export default function DelaySeverityPage() {
  return (
    <div className={styles.pageContainer}>
      <header>
        <TopBar
          title="Delay Severity"
          description="How much delay becomes severe, when does it concentrate, and what drives the impact?"
        />
      </header>

      <section>
        <DashboardHeader
          eyebrow="Dashboard 3 · Severity story"
          title="Delay Severity"
          description="Move beyond the number of delayed flights by showing how many become severe, when severe disruption concentrates, and which delay drivers create the greatest cumulative impact."
          tags={[
            "Severe delay ≥ 60 minutes",
            "Case volume + total minutes",
            "Investigation outcome",
          ]}
        />
      </section>

      {/* ==================================Page Guide=================================== */}
      <section>
        <PageGuide
          howToReadTitle="Start with escalation, then explain impact."
          howToReadDescription="The page distinguishes ordinary lateness from disruption that becomes operationally serious."
          storySteps={[
            {
              eyebrow: "Escalation",
              title: "How much delay becomes severe?",
              description:
                "Measure the transition from delayed to severely delayed.",
            },
            {
              eyebrow: "Concentration",
              title: "When is the pressure greatest?",
              description: "Use month and time band to locate the peak.",
            },
            {
              eyebrow: "Impact",
              title: "What creates the most minutes?",
              description: "Separate case count from cumulative delay burden.",
            },
          ]}
        />
      </section>

      {/* ==================================Executive Briefing=================================== */}
      <article className={styles.briefingSection}>
        <div className={styles.briefingContent}>
          <p className={styles.eyebrow}>Executive briefing</p>

          <h3 className={styles.headline}>
            More than one in four delayed flights becomes severely delayed, with
            late-aircraft delay producing the greatest cumulative impact.
          </h3>

          <p className={styles.summary}>
            The main operational story is not simply that delays occur. It is
            that a meaningful share escalates beyond 60 minutes, particularly in
            the summer and later departure bands. Late-aircraft delay dominates
            both case volume and total severe-delay minutes.
          </p>
        </div>

        <div className={styles.briefingPoints}>
          <InsightPoint
            number={1}
            attentionStatus={true}
            title="27.6% escalate"
            description="More than one quarter of delayed flights cross the severe threshold."
          />

          <InsightPoint
            number={2}
            title="June–July peak"
            description="Summer and evening departures carry the highest concentration."
          />

          <InsightPoint
            number={3}
            title="Late aircraft dominates"
            description="It creates the greatest case volume and cumulative minutes."
          />
        </div>
      </article>

      {/* ==================================Escalation Section=================================== */}
      <section>
        <SectionHeader
          title="1. How much delay becomes severe?"
          description="The escalation rate gives context before the detailed distribution."
        />

        <div className={styles.kpisContent}>
          <KpiCard
            title="Delayed flights"
            number={1_210_000}
            description="20.9% of all flights."
          />

          <KpiCard
            title="Delayed flights becoming severe"
            rate={27.6}
            description="2.2 pts above spring average."
          />

          <KpiCard
            title="Severe-delay volume"
            number={334_000}
            description="+18K cases against prior month."
          />

          <KpiCard
            title="Total severe-delay minutes"
            number={31_800_000}
            description="+8.4% cumulative operational impact."
          />
        </div>
      </section>
    </div>
  );
}
