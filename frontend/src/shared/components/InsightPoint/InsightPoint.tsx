import styles from "./InsightPoint.module.css";

export default function InsightPoint({
  number,
  title,
  description,
  attentionStatus = false,
}: {
  number: number;
  title: string;
  description: string;
  attentionStatus?: boolean;
}) {
  return (
    <div className={styles.insightPoint}>
      <span className={`${styles.number} ${attentionStatus ? styles.attention : ""}`}>{number}</span>
      <div className={styles.textContent}>
        <h3 className={styles.title}>{title}</h3>
        <p className={styles.description}>{description}</p>
      </div>
    </div>
  );
}
