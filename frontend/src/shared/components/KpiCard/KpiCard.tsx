import styles from "./KpiCard.module.css";
import { formatCompactNumber } from "../../../Utils/formatters";

type KpiCardProps =
  | {
      title: string;
      number: number | undefined;
      rate?: never;
      description?: string;
      fontSize?: string;
    }
  | {
      title: string;
      rate: number | undefined;
      number?: never;
      description?: string;
      fontSize?: string;
    };

export default function KpiCard({
  title,
  rate,
  number,
  description,
  fontSize,
}: KpiCardProps) {
  return (
    <div className={styles.kpiCard}>
      <p className={styles.title}>{title}</p>

      <h3 style={fontSize ? { fontSize } : undefined} className={styles.kpi}>
        {number !== undefined
          ? formatCompactNumber(number)
          : rate !== undefined
            ? `${rate}%`
            : "Loading..."}
      </h3>

      {description && <p className={styles.description}>{description}</p>}
    </div>
  );
}
