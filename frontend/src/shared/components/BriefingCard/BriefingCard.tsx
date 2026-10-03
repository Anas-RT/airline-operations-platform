import styles from "./BriefingCard.module.css";
import InsightPoint from "../InsightPoint/InsightPoint";
type insightPointProps = {
  number: number;
  attentionStatus?: boolean;
  title: string;
  description: string;
};
type briefingCardProps = {
  briefingHeadline: string;
  briefingSummary: string;
  insightPoints: insightPointProps[];
};

export default function BriefingCard(props: briefingCardProps) {
  return (
    <article className={styles.briefingCard}>
      <div className={styles.briefingContent}>
        <p className={styles.briefingEyebrow}>Executive briefing</p>
        <h3 className={styles.briefingHeadline}>{props.briefingHeadline}</h3>
        <p className={styles.briefingSummary}>{props.briefingSummary}</p>
      </div>
      <div className={styles.briefingPoints}>
        {props.insightPoints.map((point) => (
          <InsightPoint
            key={point.number}
            number={point.number}
            attentionStatus={point.attentionStatus}
            title={point.title}
            description={point.description}
          />
        ))}
      </div>
    </article>
  );
}
