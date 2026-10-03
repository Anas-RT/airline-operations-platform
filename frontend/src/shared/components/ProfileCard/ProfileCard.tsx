import KpiCard from "../KpiCard/KpiCard";
import styles from "./ProfileCard.module.css";

type Kpi =
  | {
      title: string;
      value: number;
      rate?: never;
    }
  | {
      title: string;
      rate: number;
      value?: never;
    };

type ProfileCardProps = {
  eyebrow?: string;
  title: string;
  description: string;
  kpis: [Kpi, Kpi, Kpi, Kpi];
};

export default function ProfileCard({
  eyebrow,
  title,
  description,
  kpis,
}: ProfileCardProps) {
  return (
    <div className={styles.profileCard}>
      <div className={styles.profileCardTarget}>
        {eyebrow && <h4 className={styles.eyebrow}>{eyebrow}</h4>}
        <h3 className={styles.title}>{title}</h3>
        <p className={styles.description}>{description}</p>
      </div>

      <div className={styles.profileCardKpisContainer}>
        {kpis.map((kpi, index) => (
          <KpiCard
            key={index}
            title={kpi.title}
            {...("value" in kpi ? { number: kpi.value } : { rate: kpi.rate })}
            fontSize="1.1rem"
          />
        ))}
      </div>
    </div>
  );
}
